using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksOnHit : MonoBehaviour {
    public Block self;
    // When the 2d blocks are hit, they will make a sound

    public void OnMouseDown()
    {
        if (SoundManager.Instance != null)
        {
            if (self.selected)
            {
                SoundManager.Instance.playBlockSound();
            }
        }
        else
        {
            Debug.Log("Cannot play playBlockSound(), SoundManager is null");
        }
    }
}
