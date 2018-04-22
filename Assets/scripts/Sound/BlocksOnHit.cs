using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksOnHit : MonoBehaviour {
    
    // When the 2d blocks are hit, they will make a sound
    public void OnMouseDown()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.playBlockSound();
        }
        else
        {
            Debug.Log("Cannot play playBlockSound(), SoundManager is null");
        }
    }
}
