using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject block;

    private List<GameObject> blocks = new List<GameObject>();
    private GameObject genStart, genNext;

    int rows = 0, col = 0; 
	// Use this for initialization
	void Start () {
        block = Resources.Load("Block") as GameObject;
        genGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void genBlock() {
        System.Random rand = new System.Random();

        genStart = GameObject.FindGameObjectWithTag("Gen_Start");
        genNext = genStart;

        for (int i = 0; i < 50; i++)
        {
            blocks.Add(Instantiate(block));

            blocks[blocks.Count - 1].transform.position = genNext.transform.position;
            blocks[blocks.Count - 1].transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);
            blocks[blocks.Count - 1].GetComponent<Renderer>().material.color = getRandomColor(); 

            genNext.transform.position += new Vector3(0.25F, 0, 0);

            col++;
            if (col == 10)
            {
                rows++;
                col = 0;
                genNext.transform.position = new Vector3(genNext.transform.position.x - (0.25F * 10), genNext.transform.position.y - 0.25F, 0);

            }
        }
    }
   
    void genGrid() {
        genStart = GameObject.FindGameObjectWithTag("Gen_Start");
        genNext = genStart;

        for(int i = 0; i < 100; i++) {

            blocks.Add(Instantiate(block));
            blocks[i].transform.position = genNext.transform.position;
            blocks[i].transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);

            blocks[i].GetComponent<Renderer>().material.color = getRandomColor();

            genNext.transform.position += new Vector3(0.25F, 0, 0);

            col++;
            if (col == 10)
            {
                rows++;
                col = 0;
                genNext.transform.position = new Vector3(genNext.transform.position.x - (0.25F * 10), genNext.transform.position.y - 0.25F, 0);

            }
        }
    }

    Color getRandomColor()
    {
        int color = Random.Range(-1, 101);

        if (color <= 40) { return Color.green; }
        else if (color > 40 && color < 60) { return Color.blue; }
        else if (color > 60 && color < 80) { return Color.red; }
        else if (color > 80 && color < 90) { return Color.gray; }
        else if (color >= 90) { return Color.magenta; }
        else { return Color.white; }
    }
}
