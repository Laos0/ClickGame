using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager> {

    protected MainGameManager() { } 

    private static bool created = false;
    public int rowCount = 0;
    public GameObject rootCanvas,
                      genStart,
                      genNext;

    public Text scoreTxt;
    public float currency;
    public bool isGameStart,
                counterStarted,
                blockSelected;

    public Vector2 playerLocation;
    public List<Block> grid;
    public Block currentBlock,
                 instBlock;

    public float getCurrency() { return currency; }


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);

        }
    }

    void Start()
    {
        // When game starts currency adds
        startCurrencyCounter();
        instBlock = Resources.Load("Block", typeof(Block)) as Block;
        genGrid();
    }

    private void Update()
    {
        if(currentBlock != null && !counterStarted)
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
        while(isGameStart)
        {
            addToCurrency(1);
            //Debug.Log(this.currency);
            yield return new WaitForSeconds(1);
        
        }
    }

    public void resetCurrency()
    {
        currency = 0;
    }

    public void addToCurrency(float value)
    {
        currency += value;
        updateCurrencyUI();
    }

    public void subToCurrency(float value)
    {
        currency -= value;
        updateCurrencyUI();
    }

    private void updateCurrencyUI()
    {
        if(scoreTxt != null)
        {
            scoreTxt.text = "" + currency;
        }
    }

    public Block getNextBlock()
    {
        Vector2 curPos = playerLocation;
        if(curPos.x < 9)
        {
            curPos.x++;
        }

        for(int i = 0; i < grid.Count; i++)
        {
            if(curPos == grid[i].getLocation())
            {
                grid[i].isClick = true;
                grid[i].selected = true;
                return grid[i];
            }
        }

        return null;
    }

    public List<Block> getSurrondingBlocks()
    {
        List<Block> surroundingBlocks = new List<Block>();
        int curX = (int)playerLocation.x;
        int curY = (int)playerLocation.y;

        if (curY == 0)
        {
            if(curX == 0)
            {
                surroundingBlocks.Add(getBlock(new Vector2(curX + 1, curY)));
                surroundingBlocks.Add(getBlock(new Vector2(curX, curY + 1)));
                surroundingBlocks.Add(getBlock(new Vector2(curX + 1, curY + 1)));
            }
            else if(curX == 9)
            {
                surroundingBlocks.Add(getBlock(new Vector2(curX - 1, curY)));
                surroundingBlocks.Add(getBlock(new Vector2(curX - 1, curY + 1)));
                surroundingBlocks.Add(getBlock(new Vector2(curX, curY + 1)));
            }

        }

        for(int i = 0; i < surroundingBlocks.Count; i++)
        {
            //surroundingBlocks[i].GetComponent<Renderer>().material.color = surroundingBlocks[i].lightUp;
        }
        return surroundingBlocks;
    }

    private void genGrid()
    {
        int row = 0,
            col = 0;

        genStart = GameObject.FindGameObjectWithTag("Gen_Start");
        genNext = genStart;

        for(int i = 0; i < 100; i++)
        {
            grid.Add(Object.Instantiate(instBlock));

            grid[i].transform.position = genNext.transform.position;
            grid[i].transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);
            grid[i].GetComponent<Renderer>().material.color = getRandomColor();
            grid[i].color = grid[i].GetComponent<Renderer>().material.color;

            genNext.transform.position += new Vector3(0.25F, 0, 0);

            grid[i].xCord = col;
            grid[i].yCord = row;
            grid[i].setReward();
            col++;

            if(i < 10)
            {
                grid[i].isClick = true;
            }
            else
            {
                grid[i].isClick = false;
            }

            if(col == 10)
            {
                row++;
                col = 0;
                genNext.transform.position = new Vector3(genNext.transform.position.x - (0.25F * 10), genNext.transform.position.y - 0.25F, 0);
            }
            Debug.Log(grid[i].getLocation());
        }
    }

    Color getRandomColor()
    {
        int color = Random.Range(-1, 101);

        if (color <= 40) { return Color.green; }
        else if (color > 40 && color < 60) { return Color.blue; }
        else if (color > 60 && color < 80) { return Color.red; }
        else if (color > 80 && color < 95) { return Color.gray; }
        else if (color >= 95) { return Color.magenta; }
        else { return Color.white; }
    }

    public Block getBlock(Vector2 location)
    {
        for(int i = 0; i < grid.Count; i++)
        {
            if(grid[i].getLocation() == location)
            {
                return grid[i];
            }
        }
        return null;
    }

    public int getBlockIndex(Block block)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            if(grid[i] == block)
            {
                return i;
            }
        }

        return 0;
    }
}


