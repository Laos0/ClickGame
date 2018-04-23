// This class is the parent class for all Block objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    protected int clicksNeeded;  // The number of clicks needed to break this block
    
    protected int reward;  // The number of points the player gets for breaking this block

    public float multiplier;

    public Transform location;
    public bool isClick;
    public bool selected; 

    Block(int clicks, int reward)
    {
        this.clicksNeeded = clicks;
        this.reward = reward;
        this.multiplier = 1;
    }

    // Use this for initialization
    void Start () {
        clicksNeeded = 10;
        reward = 50;
        multiplier = 1;
        location = this.transform;
        isClick = false;
        selected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(clicksNeeded <= 0)
        {
            Object.Destroy(gameObject);
        }
	}

    // Getters
    public int getReward() { return reward; }
    public int getClicksNeeded() { return clicksNeeded; }
    public void setReward(int reward) { this.reward = reward; }
    public void setClicks(int clicks) { this.clicksNeeded = clicks; }

    void OnMouseDown()
    {
        if(isClick)
        {
            if(selected)
            {
                clicksNeeded--;
            }
        }
        Debug.Log(clicksNeeded);
    }
}
