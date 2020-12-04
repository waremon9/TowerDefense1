using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public PinkOne _pi;
    public PurpleOne _pu;
    public GreenOne _gr;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//Start a new wave when space is pressed
        {
            StartNewWave();
        }
    }

    private void SpawnSolo(Enemy monster)//spawn a new monster a the begining of the track
    {
        Enemy E = Instantiate(monster, transform.parent);
        E.transform.position = transform.position;
        GameManager.Instance.AddEnnemy(E);//add it to the enemy alive list
    }

    private IEnumerator CoroutineSpawnGroup(EnemyGroup group)//coroutine to spawn a group of enemy
    {
        bool first = true;
        for(int i = 0; i<group.SizeOfGroup; i++)
        {
            if (first) first = false;
            else yield return new WaitForSeconds(group.TimeBetweenEnemy);//skip the first wait  to have wait only between enemy of the same group

            SpawnSolo(group.Enemy);//spawn an enemy
        }
    }

    private IEnumerator CoroutineWave(Wave wave)//coroutine to manage a full wave of enemy
    {
        foreach (EnemyGroup group in wave.WaveComposition)
        {
            StartCoroutine(CoroutineSpawnGroup(group));
            yield return new WaitForSeconds(group.SizeOfGroup * group.TimeBetweenEnemy);//wait for the end of the group before sending another
        }
        GameManager.Instance.WaveEnded = true;//update value
    }

    public void StartNewWave()
    {
        if (GameManager.Instance.enemiesList.Count != 0) return; //last wave not finished yet
        GameManager.Instance.RewardObtained = false;//reset value
        StartCoroutine(CoroutineWave(GameManager.Instance.ActualLevel.GetNextWave()));//get the wave to launch and start the coroutine
    }

}
