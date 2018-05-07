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

    public Color32 color1 = new Color32(49, 130, 189, 255);
    public Color32 color2 = new Color32(254, 178, 76, 255);
    public Color32 color3 = new Color32(189, 0, 38, 255);
    public Color32 color4 = new Color32(37, 37, 37, 255);
    public Color32 color5 = new Color32(0, 109, 44, 255);
    public Color32 color6 = new Color(247, 104, 161, 255);

    public float getCurrency() { return currency; }

    /// <summary>
    /// Keepe track of the current block destroyed
    /// </summary>
    public int countBlock;
	public Text dropText;


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
        countBlock = 0;
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
            playerLocation = currentBlock.getLocation();
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
            addToCurrency(1 * countBlock);
            //Debug.Log(this.currency);
            yield return new WaitForSeconds(1);
            if(currentBlock != null)
            {
                currentBlock.hit();
            }
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

    public Block getNextBlock(Block curBlock)
    {
        Vector2 loc = curBlock.getLocation();
        loc.x++;
        if(loc.x == 10)
        {
            loc.x = 0;
            loc.y++;
        }
        getBlock(loc).selected = true;
        return getBlock(loc);
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
        // Debug.Log(color);

        if (color <= 20) { return color1; }
        else if (color > 20 && color < 40) { return color2; }
        else if (color > 40 && color < 60) { return color3; }
        else if (color > 60 && color < 80) { return color4; }
        else if (color > 80 && color < 90) { return color5; }
        else if (color >= 90) { return color6; }

        // yellow red blue black and white are all pretty distinct from each other in all types of color blindness

        else { return Color.white; }
    }

    public Block getBlock(Vector2 location)
    {
        int index = 0;
        for(int i = 0; i < grid.Count; i++)
        {
            if(grid[i].getLocation() == location)
            {
                index = i;
            }
        }
        return grid[index];
    }

    public int getBlockIndex(Vector2 loc)
    {
        int index = 0;
        for (int i = 0; i < grid.Count; i++)
        {
            if(grid[i].getLocation() == loc)
            {
                index =  i;
                Debug.Log("THIS  " + index);
            }
        }

        return index;
    }

	//set drop text
	public void updateDropText(string dropSize)
	{
		if (dropSize == "small")
		{
			dropText.text = "Random Drop: Small Gem: + 50!";
		}
		else if (dropSize == "med")
		{
			dropText.text = "Random Drop: Medium Gem: + 100!";
		}
		else if (dropSize == "large")
		{
			dropText.text = "Random Drop: Large Gem: +200!";
		}
		else if (dropSize == "nothing")
		{
			dropText.text = "Random Drop: Nothing.";
		}
	}
}


