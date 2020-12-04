using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QteBulletUpgrade : Upgrade
{
    public int UP;
    public QteBulletUpgrade(int _cost, int up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Increase Tower's Bullet per shot by " + UP + ".\n" + base.ToString();
    }
}
