using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadUpgradeMG : Upgrade
{
    public float UP;
    public SpreadUpgradeMG(int _cost, float up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Reduce bullet Spread by " + UP + "%.\n" + base.ToString();
    }
}
