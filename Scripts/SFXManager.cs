using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //Singleton
    public static SFXManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    //End Singleton

    public List<AudioClip> deathAudio;
    public List<AudioClip> boomAudio;

    public void PlayMonsterDeathSfx()
    {
        //play a random sfx
        AudioClip tmp = deathAudio[Random.Range(0, deathAudio.Count)];
        this.GetComponent<AudioSource>().PlayOneShot(tmp);
    }
    public void PlayExplosionSfx()
    {
        //play a random sfx
        AudioClip tmp = boomAudio[Random.Range(0, boomAudio.Count)];
        this.GetComponent<AudioSource>().PlayOneShot(tmp);
    }
}
