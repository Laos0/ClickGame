using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager> {

    protected MainGameManager() { } 

    private static bool created = false;

    public GameObject rootCanvas;
    public Text scoreTxt;

    public float currency;

    public bool isGameStart, counterStarted;

    public Block currentBlock;

    public List<Block> grid;

    public bool blockSelected;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
            
        }
    }

    private void Update()
    {
        if (currentBlock != null && !counterStarted)
        {
            startCurrencyCounter();
            counterStarted = true;
        }

        for(int i = 0; i < grid.Count; i++)
        {
            if(grid[i].selected)
            {
                blockSelected = true;
            }
        }

        if(blockSelected)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if(!grid[i].selected)
                {
                    grid[i].isClick = false;
                }
            }
        }
    }

    public void startCurrencyCounter()
    {
        isGameStart = true;
        StartCoroutine(addToCurrency());
    }

    IEnumerator addToCurrency()
    {
        while (isGameStart)
        {
            // Debug.Log("OnCoroutine: " + (int)Time.time);
            if (currentBlock != null)
            {
                currentBlock.hit();
                yield return new WaitForSeconds(1);
            }
            else
            {
               //wait for next selection
            }
        }
    }

    public void resetCurrency()
    {
        currency = 0;
    }


    // Everytime a block is hit, the currency is increased based off the "value" that block contains
    public void addToCurrency(int value)
    {
        currency += value;
        updateCurrencyUI();
    }


    // Update the currency on the UI 
    private void updateCurrencyUI()
    {
        if (scoreTxt != null)
        {
            scoreTxt.text = "" + currency;
        }
        else
        {
            Debug.Log("scoreTxt equals null");
        }
      
        //Debug.Log(currency);
    }

    // Each item has a value, and currency will be deducted based off the value of that item, once purchased.
    public void subToCurrency(int value)
    {
        currency -= value;
        if(currency < 0)
        {
            currency = 0;
        }
        updateCurrencyUI();
    }

    public Block getNextBlock()
    {
        Vector2 currentLocation = new Vector2(currentBlock.xCord, currentBlock.yCord);
        if(currentLocation.x < 9)
        {
            currentLocation.y++;
        }

        for(int i = 0; i < grid.Capacity; i++)
        {
            if (currentLocation == grid[i].getLocation())
            {
                grid[i].isClick = true;
                grid[i].selected = true;
                return grid[i];
            }
        }

        return null;
    }
    
    // Return the current currency value
    public float getCurrency () { return currency; }
}

