using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Tower
{
    public float ExplosionRange;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRangeIndicator();//resize the range indicator (1-1 by default)
        SetAllUpgrade();
    }

    private void SetAllUpgrade()
    {
        //Path 1

        //Increase damage by 2, cost 300
        ArrayUpgradeAvailable[0, 0] = new DamageUpgrade(300, 2, Upgrade.type.Damage);
        //Increase explosion radius by 15%, cost 150
        ArrayUpgradeAvailable[0, 1] = new ExplosionRadiusUpgrade(150, 15, Upgrade.type.ExplosionRadius);
        //Increase damage by 3, cost 500
        ArrayUpgradeAvailable[0, 2] = new DamageUpgrade(500, 3, Upgrade.type.Damage);
        //Increase explosion radius by 20%, cost 400
        ArrayUpgradeAvailable[0, 3] = new ExplosionRadiusUpgrade(400, 20, Upgrade.type.ExplosionRadius);

        //Path 2

        //Increase range by 15%, cost 150
        ArrayUpgradeAvailable[1, 0] = new RangeUpgrade(150, 15, Upgrade.type.Range);
        //reduce fire cooldown by 12%, cost 220
        ArrayUpgradeAvailable[1, 1] = new ReloadUpgrade(220, 12, Upgrade.type.Reload);
        //reduce fire cooldown by 15%, cost 350
        ArrayUpgradeAvailable[1, 2] = new ReloadUpgrade(350, 15, Upgrade.type.Reload);
        //Increase range by 15%, cost 300
        ArrayUpgradeAvailable[1, 3] = new RangeUpgrade(300, 15, Upgrade.type.Range);

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
        bullet.GetComponent<ExplosiveBullet>().SetExplosionRange(ExplosionRange);
    }
    public override string ToString()
    {
        return base.ToString() + "Radius : " + ExplosionRange;
    }
}
