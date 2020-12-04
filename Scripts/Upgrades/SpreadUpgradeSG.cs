using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadUpgradeSG : Upgrade
{
    public float UP;
    public SpreadUpgradeSG(int _cost, float up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Reduce bullet Spread by " + UP + "%.\n" + base.ToString();
    }
}
