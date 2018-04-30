using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Block block;

    private List<Block> blocks = new List<Block>();
    private GameObject genStart, genNext;
    private float inRow;
    private bool genMore = false;
    int rows = 0, col = 0; 
	// Use this for initialization
	void Start () {
        block = Resources.Load("Block", typeof(Block)) as Block;
        genGrid();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pLoc = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().playerLocation;

        if (pLoc.x > 0)
        {
            if(pLoc.x %5 == 1)
            {
                genMore = false;
            }
            if (pLoc.x % 5 == 0 && !genMore)
            {
                Debug.Log("GENERATING");
                genBlock();
            }
        }
    }

    void genBlock()
    {
        genMore = true;
        List<Block> grid = GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().grid;
        genNext = genStart;
        //genNext.transform.position += new Vector3(rows * 0.25F, 0, 0);
        for(int i = 0; i < 50; i++)
        {
            grid.Add(Object.Instantiate(block));
            grid[grid.Count - 1].transform.position = genNext.transform.position;
            grid[grid.Count - 1].transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);

            grid[grid.Count - 1].GetComponent<Renderer>().material.color = getRandomColor();
            grid[grid.Count - 1].color = grid[grid.Count - 1].GetComponent<Renderer>().material.color;
            genNext.transform.position += new Vector3(0.25F, 0, 0);

            grid[grid.Count - 1].xCord = rows;
            grid[grid.Count - 1].yCord = col;
            grid[grid.Count - 1].setReward();
            col++;
            
            grid[grid.Count - 1].isClick = false;
            
            if (col == 10)
            {
                rows++;
                col = 0;
                genNext.transform.position = new Vector3(genNext.transform.position.x - (0.25F * 10), genNext.transform.position.y - 0.25F, 0);

            }
        }
        GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().grid = grid;
    }
   
    void genGrid() {
        genStart = GameObject.FindGameObjectWithTag("Gen_Start");
        genNext = genStart;

        for(int i = 0; i < 100; i++) {
            blocks.Add(Object.Instantiate(block));
            blocks[i].transform.position = genNext.transform.position;
            blocks[i].transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);

            blocks[i].GetComponent<Renderer>().material.color = getRandomColor();
            blocks[i].color = blocks[i].GetComponent<Renderer>().material.color;
            genNext.transform.position += new Vector3(0.25F, 0, 0);

            blocks[i].xCord = rows;
            blocks[i].yCord = col;
            blocks[i].setReward();
            col++;
            if(i < 10)
            {
                blocks[i].isClick = true;
            }
            else
            {
                blocks[i].isClick = false;
            }
            if (col == 10)
            {
                rows++;
                col = 0;
                genNext.transform.position = new Vector3(genNext.transform.position.x - (0.25F * 10), genNext.transform.position.y - 0.25F, 0);

            }
        }
        GameObject.FindGameObjectWithTag("GM").GetComponent<MainGameManager>().grid = blocks;
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
}
