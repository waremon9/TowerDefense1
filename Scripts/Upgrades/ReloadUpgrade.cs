using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUpgrade : Upgrade
{
    public float UP;
    public ReloadUpgrade(int _cost, float up, type _type) : base(_cost, _type)
    {
        UP = up;
    }

    public override string ToString()
    {
        return "Decrease Tower's Reload time by " + UP+"%" + ".\n" + base.ToString();
    }
}
