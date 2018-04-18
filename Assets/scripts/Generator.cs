using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public List<GameObject> blocks = new List<GameObject>();
    public GameObject genStart, genNext;

    int rows = 0, col = 0; 
	// Use this for initialization
	void Start () {
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

            blocks.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
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

    Color getRandomColor()
    {
        System.Random rand = new System.Random();
        int color = rand.Next(0, 4);

        switch (color)
        {
            case 0:
                return Color.green;
            case 1:
                return Color.blue;
            case 2:
                return Color.red;
            case 3:
                return Color.gray;
            case 4:
                return Color.magenta;
            default:
                return Color.white;
        }
    }
   
    void genGrid() {
        genStart = GameObject.FindGameObjectWithTag("Gen_Start");
        genNext = genStart;

        for(int i = 0; i < 100; i++) {

            blocks.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
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
}
