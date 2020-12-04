using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBullet : Bullet
{

    public ParticleSystem PS;

    private int PiercePower = 0;

    public override void GiveBulletParam(int _dmg, Vector3 dir, float range, float speed)//pas trouver mieux comme façon de faire même si je suis sure que ca existe
    {
        Dmg = _dmg; Direction = dir; DistanceTravelLeft = range; BulletSpeed = speed;
    }

    private void Update()
    {
        UpdateBullet();

        if (DistanceTravelLeft <= 0) Destroy(gameObject);//max range reached, destroy bullet
    }

    public override void UpdateBullet()
    {
        Vector3 tmp = transform.position;

        //bullet go forward
        transform.position += transform.forward * Time.deltaTime * BulletSpeed;

        DistanceTravelLeft -= Vector3.Distance(tmp, transform.position);//update travel distance for max range check

    }

    private void OnTriggerEnter(Collider other)//bullet hit something
    {
        if (other.gameObject.CompareTag("Enemy"))//it's a monster
        {
            Instantiate(PS, transform.position, transform.rotation, GameManager.Instance.ParticlesParents.transform).Play();

            other.gameObject.GetComponent<Enemy>().Hit(Dmg);//monster loose hp

            if(PiercePower--<=0) Destroy(gameObject);//bullet is destroyed
        }
    }

    public void setPierce(int pierce)//if tower has piercing power
    {
        PiercePower = pierce;
    }
}
