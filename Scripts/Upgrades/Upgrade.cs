using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Le systeme d'upgrade est moche, mais j'ai pas réussi à faire ce que je voulais et ce "truc" fonctionne.

public abstract class Upgrade
{
    public int Cost;
    public enum type {Damage, Range, Reload, Pierce, ExplosionRadius, SpreadMG, SpreadSG, QteBullet};
    public type Type;

    public Upgrade(int _cost, type _type)
    {
        Cost = _cost;
        Type = _type;
    }

    public override string ToString() 
    {
        return "cost: " + Cost;
    }
}
