using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Block : MonoBehaviour
{
    public float reward,
                 multiplier,
                 clicksNeeded;

    public int xCord,
               yCord;

    public bool isClick,
                selected,
                isDestructionParticleExist,
                isClickParticleExist;

    public Color color,
                 litUp;

	public GameObject destructionParticle,
					  clickParticle;

	public bool hasNotDropped;
	//public Text dropText; Will Fix

   // private ParticleSystem clickEffect;

    void Start()
    {
        //litUp = Color.magenta;
        clicksNeeded = Random.Range(5, 30);
        selected = false;
        multiplier = 1;
        destructionParticle.GetComponent<Renderer>().sortingLayerName = "Foreground";
		hasNotDropped = true;
    }
	// Update is called once per frame
    void Update()
    {
        if (clicksNeeded <= 0)
        {
			if(hasNotDropped)
			{
                destroyBlock();
                getRandomReward();
				hasNotDropped = false;
			}
        }
        
        if(selected)
        {
            enableOutline();
            MainGameManager.Instance.playerLocation = getLocation();
        }
        else
        {
            disableOutline();
        }
    }

    void destroyBlock()
    {
        //GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().grid.RemoveAt(GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getBlockIndex(GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getBlock(new Vector2(xCord, yCord))));
        MainGameManager.Instance.grid.RemoveAt(MainGameManager.Instance.grid.IndexOf(MainGameManager.Instance.currentBlock));
        GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(reward);
        Block tmp = MainGameManager.Instance.currentBlock;
        MainGameManager.Instance.currentBlock = MainGameManager.Instance.getNextBlock(tmp);

        selected = false;

        if (!isDestructionParticleExist)
        {
            //GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(this.reward);
            MainGameManager.Instance.addToCurrency(this.reward);

            //GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().getNextBlock();
            //MainGameManager.Instance.currentBlock = MainGameManager.Instance.getNextBlock();
            //MainGameManager.Instance.getSurrondingBlocks();
            // When the blocks are destroyed, particle will spawn at their location and a sound will be played.


            GameObject destructionEffect = Instantiate(destructionParticle, getOffSetSpawnPosition(gameObject.transform.position), gameObject.transform.rotation);
            destructionEffect.transform.parent = gameObject.transform;
            SoundManager.Instance.playCrumbleSound();
            isDestructionParticleExist = true;

            MainGameManager.Instance.countBlock++;

            Destroy(gameObject, .2f);
        }
    }

    void enableOutline()
    {
        gameObject.GetComponent<Outline>().enabled = true;
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    void disableOutline()
    {
        this.GetComponent<Outline>().enabled = false;
        this.GetComponent<Renderer>().material.color = color;
    }

    public Vector2 getLocation()
    {
        return new Vector2(xCord, yCord);
    }

    public void setClicks(int clicks)
    {
        this.clicksNeeded = clicks;
    }

    void OnMouseDown()
    {
        if (isClick)
        {
            // Not needed for now
           /* if (!isClickParticleExist)
            {
                clickEffect = Instantiate(clickParticle, new Vector3(), gameObject.transform.rotation).GetComponent<ParticleSystem>();
                clickEffect.transform.parent = gameObject.transform;
                isClickParticleExist = true;
            }
            else
            {
                if (!clickEffect.isPlaying)
                {
                    clickEffect.Play();
                }
            }
            */

            if (!selected)
            {
                selected = true;
                MainGameManager.Instance.playerLocation = getLocation();
                MainGameManager.Instance.currentBlock = this;
                GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().currentBlock = this;
            }
            else
            {
                this.clicksNeeded -= MainGameManager.clickValue;
                GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().addToCurrency(1 * (int)this.multiplier);
            }
        }
        else
        {

        }
    }

    public void hit()
    {
        clicksNeeded -= 1 * (int)this.multiplier;
        if (MainGameManager.Instance != null)
        {
            MainGameManager.Instance.addToCurrency(1 * (int)this.multiplier);
        }
    }

    public void setReward()
    {
        if (color == new Color32(49, 130, 189, 255)) { reward = 50; }
        else if (color == new Color32(254, 178, 76, 255)) { reward = 75; }
        else if (color == new Color32(189, 0, 38, 255)) { reward = 150; }
        else if (color == new Color32(37, 37, 37, 255)) { reward = 200; }
        else if (color == new Color32(0, 109, 44, 255)) { reward = 300; }
        else { reward = 50; }
    }

    public float getReward()
    {
        return reward;
    }

    public float getClicksNeeded()
    {
        return clicksNeeded;
    }

    private Vector3 getOffSetSpawnPosition(Vector3 position)
    {
        Vector3 spawnPosition = new Vector3();
        Vector3 gp = position;
        int offSet = -5;
        spawnPosition.Set(gp.x, gp.y, gp.z + offSet);

        return spawnPosition;
    }

	//This methoed is called when a block is destroyed to see of a random item will drop for more rewards
	public void getRandomReward()
	{		
			int spawnChance = Random.Range(0, 100);


		//Check if something spawns
		if (spawnChance >= 0 && spawnChance < 30)
		{
			Debug.Log("You got a small Gem: + 50 points");
			MainGameManager.Instance.addToCurrency(50);
			MainGameManager.Instance.updateDropText("small");

		}
		else if (spawnChance >= 30 && spawnChance < 45)
		{
			Debug.Log("You got a medium Gem: + 100 points");
			MainGameManager.Instance.addToCurrency(100);
			MainGameManager.Instance.updateDropText("med");
		}
		else if (spawnChance >= 45 && spawnChance < 50)
		{
			Debug.Log("You got a large Gem: + 200 points");
			MainGameManager.Instance.addToCurrency(200);
			MainGameManager.Instance.updateDropText("large");
		}
		else
		{
			MainGameManager.Instance.updateDropText("nothing");
		}
		
	}
}
