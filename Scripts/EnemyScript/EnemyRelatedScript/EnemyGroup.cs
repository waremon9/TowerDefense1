using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup
{
    public Enemy Enemy;
    public int SizeOfGroup;
    public float TimeBetweenEnemy;

    //A group of ennemie is composed of an enemy type, the number of them in the group and the time that separate them when they spawn
    public EnemyGroup(Enemy E, int qte, float TimeBetween)
    {
        Enemy = E;
        SizeOfGroup = qte;
        TimeBetweenEnemy = TimeBetween;
    }
}
