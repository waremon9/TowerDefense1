using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Tower
{
    public GameObject cannon;

    // Start is called before the first frame update
    void Start()
    {
        Range = 100;
        SetAllUpgrade();
    }

    private void SetAllUpgrade()
    {
        //Path 1

        //Increase damage by 2, cost 300
        ArrayUpgradeAvailable[0, 0] = new DamageUpgrade(300, 2, Upgrade.type.Damage);
        //Increase damage by 3, cost 500
        ArrayUpgradeAvailable[0, 1] = new DamageUpgrade(500, 3, Upgrade.type.Damage);
        //Increase damage by 5, cost 1000
        ArrayUpgradeAvailable[0, 2] = new DamageUpgrade(1000, 5, Upgrade.type.Damage);
        //Increase damage by 8, cost 6800
        ArrayUpgradeAvailable[0, 3] = new DamageUpgrade(1600, 8, Upgrade.type.Damage);

        //Path 2

        //reduce fire cooldown by 20%, cost 230
        ArrayUpgradeAvailable[1, 0] = new ReloadUpgrade(230, 20, Upgrade.type.Reload);
        //reduce fire cooldown 20%, cost 450
        ArrayUpgradeAvailable[1, 1] = new ReloadUpgrade(450, 20, Upgrade.type.Reload);
        //reduce fire cooldown by 20%, cost 800
        ArrayUpgradeAvailable[1, 2] = new ReloadUpgrade(800, 20, Upgrade.type.Reload);
        //reduce fire cooldown 25%, cost 1250
        ArrayUpgradeAvailable[1, 3] = new ReloadUpgrade(1250, 25, Upgrade.type.Reload);

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
                target.Hit(Atk);
                _lastShot = Time.time;
                StartCoroutine(CoroutineShootRecoil());
            }
        }
    }

    public IEnumerator CoroutineShootRecoil()
    {
        Vector3 tmp = cannon.transform.localPosition;
        float OriginalPosition = tmp.z;
        while (tmp.z > OriginalPosition-0.5f)
        {
            tmp.z -= 0.1f;
            cannon.transform.localPosition = tmp;
            yield return null;
        }
        while (tmp.z < OriginalPosition)
        {
            tmp.z += 0.01f;
            if (tmp.z >= OriginalPosition) tmp.z = OriginalPosition;
            cannon.transform.localPosition = tmp;
            yield return null;
        }
    }

    public override void bulletParameter(Bullet bullet)
    {
        bullet.GiveBulletParam(Atk, transform.rotation.eulerAngles, Range, Speed);//give bullet it's classic parameter depending on tower stat
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
