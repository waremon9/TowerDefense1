using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceUpgrade : Upgrade
{
    public int UP;
    public PierceUpgrade(int _cost, int up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Increase Tower's Piercing power by "+UP+".\n"+base.ToString();
    }
}
