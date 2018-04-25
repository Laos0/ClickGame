using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class blkText : MonoBehaviour {

	// Use this for initialization
	public Text blockCount;
	public int num;
	public int timeScale;
	int time;

	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		blockCount.text = "x" + num;
		if (time * Time.deltaTime < timeScale)
			time++;
		else {
			num++;
			time = 0;
		}
	}

	void OnMouseDown(){
		num--;
		blockCount.text = "x" + num;
	}
}
