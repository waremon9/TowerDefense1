using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public List<EnemyGroup> WaveComposition;

    //A wave is constitued of group of enemy
    public Wave(List<EnemyGroup> list)
    {
        WaveComposition = list;

    }
}
