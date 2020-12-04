using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Tower : MonoBehaviour
{
    public int Atk;
    public float Range;
    public float Reload;
    public float Speed;
    public int Cost;

    protected float _lastShot = 0;

    public Bullet bullet;

    protected List<Enemy> InRange = new List<Enemy>();
    protected Enemy target;

    public bool IsActive = false;

    public bool IsColliding = false;

    public Upgrade[,] ArrayUpgradeAvailable = new Upgrade[2, 4];

    public int Path1Upgrade = 0;
    public int Path2Upgrade = 0;

    private int TowerValue;

    private void Start()
    {
        TowerValue = Cost;//trace the total tower value. base cost + upgrade
    }

    public virtual void Update()
    {
        if (IsActive)
        {
            if(InRange.Count!=0) InRange.Clear();//empty the list
                getEnemyInRange();//get all enemy in range
        }
    }

    public void ActivateTower()
    {
        IsActive = true;
    }

    protected bool CanShoot()
    {
        return _lastShot + Reload < Time.time;//return if cooldown is down or not
    }

    public void getEnemyInRange()
    {
        //get all enemy in shooting range
        foreach (Enemy enemy in GameManager.Instance.enemiesList)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < Range)//distance between enemy and turret
            {
                InRange.Add(enemy);
            }
        }
    }

    protected Enemy getClosestToEndEnemy()
    {
        List<Enemy> SortedList = InRange.OrderBy(o => o.DistanceLeft()).ToList();//sort the list by distance left to end of path
        return SortedList[0];//return the closest
    }

    protected void visualFollowTarget()
    {
        float rotateLightSpeed = 1;//to avoid magic number and because 1 made it so the rotation is instant
        //the tower rotate to look at the actual target
        Vector3 lookPos = target.transform.position - transform.position;
        lookPos.y = 0;//don't shot up or down
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateLightSpeed);

    }

    public void Shoot()
    {
        Bullet B = Instantiate(bullet, transform.forward + transform.position, transform.rotation, GameManager.Instance.bulletsParents.transform);//create bullet
        bulletParameter(B);//give the bullet its parameter according to the tower stats

        _lastShot = Time.time;//cooldown is up
    }
    public void Shoot(Vector3 spread)//shoot but with a bullet spread
    {
        Bullet B = Instantiate(bullet, transform.forward + transform.position, transform.rotation, GameManager.Instance.bulletsParents.transform);//create bullet
        B.transform.Rotate(spread);
        bulletParameter(B);//give the bullet its parameter according to the tower stats

        _lastShot = Time.time;//cooldown is up
    }

    public abstract void bulletParameter(Bullet bullet);

    protected void  UpdateRangeIndicator()
    {
        //resize the circle showing the range of the turret. Used when tower created (circle is 1-1 by default) and range increased
        float MagicNumber = 1;//just to reduce the range indicator because i think having all the target body in range to shoot feel like the indicator isn't correct.
        Vector3 NewScale = new Vector3(Range*2-MagicNumber, 0.01f, Range*2-MagicNumber);
        transform.Find("RangeIndicator").transform.localScale = NewScale;//update scale
    }

    public void ShowRangeIndicator(bool show)
    {
        //activate the gameobject used to show the tower range
        transform.Find("RangeIndicator").gameObject.SetActive(show);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Path") || other.transform.CompareTag("Tower")) IsColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsColliding = false;
    }

    public override string ToString()
    {
        return "Damage : "+Atk+"\nReload : "+Reload+"/s\n";
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.Type)
        {
            case Upgrade.type.Damage:
                Atk += ((DamageUpgrade)upgrade).UP;
                break;
            case Upgrade.type.Range:
                Range += Range*((RangeUpgrade)upgrade).UP/100;
                UpdateRangeIndicator();
                break;
            case Upgrade.type.Reload:
                Reload -= Reload*((ReloadUpgrade)upgrade).UP/100;
                break;
            case Upgrade.type.Pierce:
                ((WhiteTower)this).PiercePower += ((PierceUpgrade)upgrade).UP;
                break;
            case Upgrade.type.ExplosionRadius:
                ((BombTower)this).ExplosionRange += ((BombTower)this).ExplosionRange * ((ExplosionRadiusUpgrade)upgrade).UP / 100;
                break;
            case Upgrade.type.QteBullet:
                ((ShotGunTower)this).QteBullet += ((QteBulletUpgrade)upgrade).UP;
                break;
            case Upgrade.type.SpreadSG:
                ((ShotGunTower)this).BulletSpread -= ((ShotGunTower)this).BulletSpread * ((SpreadUpgradeSG)upgrade).UP / 100;
                break;
            case Upgrade.type.SpreadMG:
                ((MachineGunTower)this).BulletSpread -= ((MachineGunTower)this).BulletSpread * ((SpreadUpgradeMG)upgrade).UP / 100;
                break;
            default:
                Debug.LogError("Y a problème dans applyUpgrade de Tower.cs");
                break;
        }
        TowerValue += upgrade.Cost;
    }

    public bool ControlledByPlayer = false;
    //public abstract void PlayerControll();
}
