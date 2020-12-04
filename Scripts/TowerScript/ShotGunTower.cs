using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunTower : Tower
{
    public int QteBullet;
    public float BulletSpread;

    // Start is called before the first frame update
    void Start()
    {
        SetAllUpgrade();
        UpdateRangeIndicator();//resize the range indicator (1-1 by default)
    }

    private void SetAllUpgrade()
    {
        //Path 1

        //Increase range by 20%, cost 180
        ArrayUpgradeAvailable[0, 0] = new RangeUpgrade(180, 20, Upgrade.type.Range);
        //Increase damage by 1, cost 400
        ArrayUpgradeAvailable[0, 1] = new DamageUpgrade(400, 1, Upgrade.type.Damage);
        //Increase bullet qte by 3, cost 550
        ArrayUpgradeAvailable[0, 2] = new QteBulletUpgrade(550, 3, Upgrade.type.QteBullet);
        //Increase bullet qte by 10, cost 2000
        ArrayUpgradeAvailable[0, 3] = new QteBulletUpgrade(2000, 10, Upgrade.type.QteBullet);

        //Path 2

        //reduce bullet spread by 25%, cost 340
        ArrayUpgradeAvailable[1, 0] = new SpreadUpgradeSG(340, 25, Upgrade.type.SpreadSG);
        //reduce bullet spread by 25%, cost 500
        ArrayUpgradeAvailable[1, 1] = new SpreadUpgradeSG(500, 25, Upgrade.type.SpreadSG);
        //reduce fire cooldown by 20%, cost 600
        ArrayUpgradeAvailable[1, 2] = new ReloadUpgrade(600, 20, Upgrade.type.Reload);
        //reduce fire cooldown by 20%, cost 1000
        ArrayUpgradeAvailable[1, 3] = new ReloadUpgrade(1000, 20, Upgrade.type.Reload);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (InRange.Count != 0)//at least one
        {
            target = getClosestToEndEnemy();//select the closest to the end as the target
            visualFollowTarget();//look at the target
            if (CanShoot())
            {
                for (int i = 0; i < QteBullet; i++)
                {
                    Vector3 spread = Vector3.zero;
                    spread.y = Random.Range(-BulletSpread, BulletSpread);
                    Shoot(spread);//check cooldown is down and shoot
                }

            }
        }
    }

    public override void bulletParameter(Bullet bullet)
    {
        bullet.GiveBulletParam(Atk, transform.rotation.eulerAngles, Range, Speed);//give bullet it's classic parameter depending on tower stat
    }

    public override string ToString()
    {
        return base.ToString() + "Bullet quantity : " + QteBullet+"\nBullet spread : "+BulletSpread;
    }
}
