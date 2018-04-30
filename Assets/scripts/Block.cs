﻿// This class is the parent class for all Block objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public float reward,
                 multiplier,
                 clicksNeeded;

    public int xCord,
               yCord;

    public bool isClick,
                selected;

    public Color color;
    public Block nextBlock;
    
    // Destruction effects on cubes
    public GameObject destructionParticle;
    public GameObject clickParticle;
   // GameObject destructionEffect;
    private ParticleSystem clickEffect;
    public bool isDestructionParticleExist;
    public bool isClickParticleExist;
   

    void Start ()
    {
        clicksNeeded = 5;
        reward = 50;
        multiplier = 1;
        selected = false;
        // Render the particle infront of block
       destructionParticle.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }
	// Update is called once per frame
	void Update () {

            // Update is called once per frame
                if (clicksNeeded <= 0)
                {
                    GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().grid.RemoveAt(GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getBlockIndex(GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getBlock(new Vector2(xCord, yCord))));
                    Object.Destroy(gameObject);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(reward);
                    GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getNextBlock();
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


                if (selected)
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

    public Vector2 getLocation()
    {
        return new Vector2(xCord, yCord);
    }

    public void setClicks(int clicks) { this.clicksNeeded = clicks; }

    void OnMouseDown()
    {
        if (isClick)
        {
            if (!selected)
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
        GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(1 * (int)this.multiplier);
    }

    public void setReward()
    {
        if (color == Color.green) { reward = 50; }
        else if (color == Color.blue) { reward = 75; }
        else if (color == Color.red) { reward = 150; }
        else if (color == Color.gray) { reward = 200; }
        else if (color == Color.magenta) { reward = 300; }
        else { reward = 50; }
    }

    public float getReward() { return reward; }
    public float getClicksNeeded() { return clicksNeeded; }

    private Vector3 getOffSetSpawnPosition(Vector3 position)
    {
        Vector3 spawnPosition = new Vector3();
        Vector3 gp = position;
        int offSet = -5;
        spawnPosition.Set(gp.x, gp.y, gp.z + offSet);

        return spawnPosition;
    }
}
