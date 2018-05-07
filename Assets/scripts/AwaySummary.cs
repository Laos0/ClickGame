using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwaySummary : MonoBehaviour {

    public Text blocksDestroyed, goldEarned, gemsFound;
    public float ticks;
    bool ticksSent = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!ticksSent)
        {
            ticks = -ConfigManager.getTime();
            sendTicks();
        }
        Debug.Log("TIME: " + ticks);
    }

    void sendTicks()
    {
        ticksSent = true;
        breakBlocks();
    }

    void breakBlocks()
    {
        float tickCount = ticks;
        List<Vector2> blocks = new List<Vector2>();

        for(int i = 0; i < MainGameManager.Instance.grid.Count; i++)
        {
            if((tickCount - MainGameManager.Instance.grid[i].clicksNeeded > 0))
            {
                tickCount -= MainGameManager.Instance.grid[i].clicksNeeded;
                blocks.Add(MainGameManager.Instance.grid[i].getLocation());
            }
        }

        blocksDestroyed.text = blocks.Count.ToString();
        goldEarned.text = ticks.ToString();
        gemsFound.text = "12";
    }
}
