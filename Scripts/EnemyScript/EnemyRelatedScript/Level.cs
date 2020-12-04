using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public List<Wave> levelWaves = new List<Wave>();
    public int levelSize;
    public int ActualWave = 0;

    //A level i sonstitued by a certain number of enemies waves.
    public Level(List<Wave> waves)
    {
        levelWaves = waves;
        levelSize = waves.Count;
    }

    public Wave GetNextWave()//return a wave and update value to give next one later
    {
        if (ActualWave == levelWaves.Count) ActualWave -= 1;//for now, loop last wave
        return levelWaves[ActualWave++];
    }
}
