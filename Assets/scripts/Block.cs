// This class is the parent class for all Block objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    // The number of clicks needed to break this block
    protected int clicksNeeded;
    // The number of points the player gets for breaking this block
    protected int reward;

    // Use this for initialization
    void Start () {
        clicksNeeded = 20;
        reward = 50;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Getters
    public int getReward() { return reward; }
    public int getClicksNeeded() { return clicksNeeded; }
}
