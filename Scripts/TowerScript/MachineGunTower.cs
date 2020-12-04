using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTower : Tower
{
    public float BulletSpread;

    public GameObject barrel;
    public float barrelSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetAllUpgrade();
        UpdateRangeIndicator();//resize the range indicator (1-1 by default)
    }

    private void SetAllUpgrade()
    {
        //Path 1

        //Increase range by 18%, cost 300
        ArrayUpgradeAvailable[0, 0] = new RangeUpgrade(300, 18, Upgrade.type.Range);
        //Increase damage by 1, cost 400
        ArrayUpgradeAvailable[0, 1] = new DamageUpgrade(400, 1, Upgrade.type.Damage);
        //Increase range by 15%, cost 56
        ArrayUpgradeAvailable[0, 2] = new RangeUpgrade(560, 15, Upgrade.type.Range);
        //reduce fire cooldown 50%, cost 7000
        ArrayUpgradeAvailable[0, 3] = new ReloadUpgrade(7000, 50, Upgrade.type.Reload);

        //Path 2

        //reduce bullet spread by 25%, cost 340
        ArrayUpgradeAvailable[1, 0] = new SpreadUpgradeMG(340, 25, Upgrade.type.SpreadMG);
        //reduce fire cooldown 10%, cost 510
        ArrayUpgradeAvailable[1, 1] = new ReloadUpgrade(510, 10, Upgrade.type.Reload);
        //reduce fire cooldown 12%, cost 950
        ArrayUpgradeAvailable[1, 2] = new ReloadUpgrade(950, 12, Upgrade.type.Reload);
        //reduce fire cooldown 15%, cost 1800
        ArrayUpgradeAvailable[1, 3] = new ReloadUpgrade(1800, 15, Upgrade.type.Reload);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (InRange.Count != 0)//at least one
        {
            rotateBarrel();

            target = getClosestToEndEnemy();//select the closest to the end as the target
            visualFollowTarget();//look at the target
            if (CanShoot())
            {
                Vector3 spread = Vector3.zero;
                spread.y = Random.Range(-BulletSpread, BulletSpread);
                Shoot(spread);//check cooldown is down and shoot
            }
        }
    }

    private void rotateBarrel()
    {
        barrel.transform.Rotate(Vector3.forward/(Reload*barrelSpeed));
    }

    public override void bulletParameter(Bullet bullet)
    {
        bullet.GiveBulletParam(Atk, transform.rotation.eulerAngles, Range, Speed);//give bullet it's classic parameter depending on tower stat
    }

    public override string ToString()
    {
        return base.ToString() + "\nBullet spread : " + BulletSpread;
    }
}
