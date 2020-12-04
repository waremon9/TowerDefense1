using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadiusUpgrade : Upgrade
{
    public float UP;
    public ExplosionRadiusUpgrade(int _cost, float up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Increase Bomb's Explosion radius by " + UP+"%" + ".\n" + base.ToString();
    }
}
