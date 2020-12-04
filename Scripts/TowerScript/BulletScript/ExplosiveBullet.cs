using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{

    public ParticleSystem PS;

    private float ExplosionRange;

    private bool exploded = false;

    public override void GiveBulletParam(int _dmg, Vector3 dir, float range, float speed)//pas trouver mieux comme façon de faire même si je suis sure que ca existe
    {
        Dmg = _dmg; Direction = dir; DistanceTravelLeft = range; BulletSpeed = speed;
    }

    private void Update()
    {
        UpdateBullet();

        if (DistanceTravelLeft <= 0) Megumin();//max range reached, auto explode then destroy
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
        if (other.gameObject.CompareTag("Enemy") && !exploded)//it's a monster
        {
            Megumin();
        }
    }

    private void Megumin()
    {
        exploded = true;

        SFXManager.Instance.PlayExplosionSfx();

        ParticleSystem parti = Instantiate(PS, transform.position, transform.rotation, GameManager.Instance.ParticlesParents.transform);//particle
        parti.transform.localScale = new Vector3(ExplosionRange/7, ExplosionRange/7, ExplosionRange/7);
        parti.Play();

        Collider[] hits = Physics.OverlapSphere(transform.position, ExplosionRange);//get all gameobject in range
        foreach (Collider collider in hits)
        {
            if (collider.gameObject.CompareTag("Enemy"))//if enemy
            {
                collider.gameObject.GetComponent<Enemy>().Hit(Dmg);//take damage
            }
            Destroy(gameObject);
        }
    }

    public void SetExplosionRange(float range)
    {
        ExplosionRange = range;
    }
}
