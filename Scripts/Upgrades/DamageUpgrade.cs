using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : Upgrade
{
    public int UP;
    public DamageUpgrade(int _cost, int up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Increase Tower's Damage by " + UP + ".\n" + base.ToString();
    }
}
