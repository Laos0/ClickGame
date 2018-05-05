using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    protected SoundManager() { } 

    public AudioSource startMenuAudio;
    public AudioSource blockOnHitAudio;
    public AudioSource crumbleAudio;
     
    /// <summary>
    /// This is for generic UI buttons, call this audio
    /// </summary>
    public AudioSource uiButtonAudio;
   

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

    public void playCrumbleSound()
    {
        crumbleAudio.Play();
    }

    public void playButtonClickSound()
    {
        uiButtonAudio.Play();
    }

}
