﻿// This class is the parent class for all Block objects

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
    
    // Destruction effects on cubes
    public GameObject destructionParticle;
    public GameObject clickParticle;
   // GameObject destructionEffect;
    private ParticleSystem clickEffect;
    public bool isDestructionParticleExist;
    public bool isClickParticleExist;
   

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
        // Render the particle infront of block
       destructionParticle.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }
	
	// Update is called once per frame
	void Update () {

		if(clicksNeeded <= 0)
        {
            if (!isDestructionParticleExist)
            {

                //GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(this.reward);
                MainGameManager.Instance.addToCurrency(this.reward);

                //GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getNextBlock();
                MainGameManager.Instance.currentBlock = MainGameManager.Instance.getNextBlock();

                // When the blocks are destroyed, particle will spawn at their location and a sound will be played.


                GameObject destructionEffect = Instantiate(destructionParticle, getOffSetSpawnPosition(gameObject.transform.position), gameObject.transform.rotation);
                destructionEffect.transform.parent = gameObject.transform;
                SoundManager.Instance.playCrumbleSound();
                isDestructionParticleExist = true;


                Destroy(gameObject, .2f);
            }


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
            if (!isClickParticleExist)
            {
                clickEffect = Instantiate(clickParticle, new Vector3(), gameObject.transform.rotation).GetComponent<ParticleSystem>();
                clickEffect.transform.parent = gameObject.transform;
                isClickParticleExist = true;
            }
            else
            {
                if (!clickEffect.isPlaying) {
                    clickEffect.Play();
                }
            }

            if (!selected)
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

    private Vector3 getOffSetSpawnPosition(Vector3 position)
    {
        Vector3 spawnPosition = new Vector3();
        Vector3 gp = position;
        int offSet = -5;
        spawnPosition.Set(gp.x, gp.y, gp.z + offSet);

        return spawnPosition;
    }
}
