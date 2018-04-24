// This class is the parent class for all Block objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    protected int clicksNeeded;  // The number of clicks needed to break this block
    protected int reward;  // The number of points the player gets for breaking this block
    public float multiplier;
    public int xCord, yCord;
    public bool isClick;
    public bool selected;
    public Color color;
    public Block nextBlock;

    Block(int clicks, int reward)
    {
        this.clicksNeeded = clicks;
        this.reward = reward;
        this.multiplier = 1;
    }

    // Use this for initialization
    void Start () {
        clicksNeeded = 50;
        reward = 50;
        multiplier = 1;
        selected = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(clicksNeeded <= 0)
        {
            Object.Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(this.reward);
            GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getNextBlock();
        }

        if(selected)
        {
            this.GetComponent<Outline>().enabled = true;
            this.GetComponent<Renderer>().material.color = color;

        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
            this.GetComponent<Renderer>().material.color = color;
        }
	}

    // Getters
    public int getReward() { return reward; }
    public int getClicksNeeded() { return clicksNeeded; }
    public void setReward(int reward) { this.reward = reward; }
    public void setClicks(int clicks) { this.clicksNeeded = clicks; }

    void OnMouseDown()
    {
        Debug.Log("CLICK");

        if (isClick) 
        {
            if(!selected)
            {
                selected = true;
                GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = this;
            }
            else
            {
                this.clicksNeeded--;
                GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(1 * (int)this.multiplier);
            }
        }
    }

    public void hit()
    {
        clicksNeeded -= 1 * (int)this.multiplier;
        Debug.Log(clicksNeeded);
        GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(1 * (int)this.multiplier);
    }

    public Vector2 getLocation()
    {
        return new Vector2(xCord, yCord);
    }
}
