// This class is the parent class for all Block objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    protected int clicksNeeded;  // The number of clicks needed to break this block
    
    protected int reward;  // The number of points the player gets for breaking this block

    Block(int clicks, int reward)
    {
        this.clicksNeeded = clicks;
        this.reward = reward; 
    }

    // Use this for initialization
    void Start () {
        clicksNeeded = 10;
        reward = 50;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Getters
    public int getReward() { return reward; }
    public int getClicksNeeded() { return clicksNeeded; }
    public void setReward(int reward) { this.reward = reward; }
    public void setClicks(int clicks) { this.clicksNeeded = clicks; }

    void OnMouseDown()
    {
        clicksNeeded--;
        Debug.Log(clicksNeeded);
    }
}
