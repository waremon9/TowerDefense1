using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected int Dmg;

    protected Vector3 Direction;
    protected float DistanceTravelLeft;
    protected float BulletSpeed;

    protected int BulletPiercePower;

    public abstract void UpdateBullet();
    public abstract void GiveBulletParam(int _dmg, Vector3 dir, float range, float speed);

}
