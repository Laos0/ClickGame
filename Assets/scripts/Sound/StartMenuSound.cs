using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSound : MonoBehaviour {

    public AudioSource buttonAudio;


    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(this);

	}

    public void playStartMenuButtonSound()
    {
        if (buttonAudio != null)
        {
            buttonAudio.Play();
        }
        else
        {
            Debug.Log("buttonAudio is null");
        }
    }
}
