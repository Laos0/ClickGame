using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    protected SoundManager() { } 

    public AudioSource startMenuAudio;
    public AudioSource blockOnHitAudio;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    public void playBlockSound()
    {
        blockOnHitAudio.Play();
    }


}
