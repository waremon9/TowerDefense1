using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MettalicOne : Enemy
{
    public int _hp;
    public int _atk;
    public float _speed;
    public bool _flying;

    public override void Start()
    {
        MaxHP = HP = _hp; Atk = _atk; Speed = _speed; Flying = _flying;
        base.Start();
    }
}
