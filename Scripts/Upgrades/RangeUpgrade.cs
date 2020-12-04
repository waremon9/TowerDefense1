using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : Upgrade
{
    public float UP;
    public RangeUpgrade(int _cost, float up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Increase Tower's Range by " + UP+"%" + ".\n" + base.ToString();
    }
}
