using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTower : Tower
{
    public int PiercePower;

    // Start is called before the first frame update
    void Start()
    {
        SetAllUpgrade();
        UpdateRangeIndicator();//resize the range indicator (1-1 by default)
    }

    private void SetAllUpgrade()
    {
        //Path 1

        //Increase range by 12%, cost 70
        ArrayUpgradeAvailable[0, 0] = new RangeUpgrade(70, 12f, Upgrade.type.Range);
        //Increase damage by 1, cost 180
        ArrayUpgradeAvailable[0, 2] = new DamageUpgrade(180, 1, Upgrade.type.Damage);
        //Increase range by 18%, cost 110
        ArrayUpgradeAvailable[0, 1] = new RangeUpgrade(110, 18f, Upgrade.type.Range);
        //Increase damage by 2, cost 320
        ArrayUpgradeAvailable[0, 3] = new DamageUpgrade(320, 1, Upgrade.type.Damage);

        //Path 2

        //Increase pierce by 1, cost 90
        ArrayUpgradeAvailable[1, 0] = new PierceUpgrade(90, 1, Upgrade.type.Pierce);
        //Increase pierce by 2, cost 170
        ArrayUpgradeAvailable[1, 1] = new PierceUpgrade(170, 2, Upgrade.type.Pierce);
        //reduce fire cooldown by 25%, cost 230
        ArrayUpgradeAvailable[1, 2] = new ReloadUpgrade(230, 25, Upgrade.type.Reload);
        //reduce fire cooldown 25%, cost 3200
        ArrayUpgradeAvailable[1, 3] = new ReloadUpgrade(320, 25, Upgrade.type.Reload);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (InRange.Count != 0)//at least one
        {
            target = getClosestToEndEnemy();//select the closest to the end as the target
            visualFollowTarget();//look at the target
            if (CanShoot()) Shoot();//check cooldown is down and shoot
        }
    }

    public override void bulletParameter(Bullet bullet)
    {
        bullet.GiveBulletParam(Atk, transform.rotation.eulerAngles, Range, Speed);//give bullet it's classic parameter depending on tower stat
        bullet.GetComponent<ClassicBullet>().setPierce(PiercePower);
    }

    public override string ToString()
    {
        return base.ToString()+"Pierce : "+PiercePower;
    }
}
