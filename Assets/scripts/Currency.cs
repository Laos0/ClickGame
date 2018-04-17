using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Currency : MonoBehaviour {

	// Use this for initialization
	public Text scoreTxt;
	public int counter;
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreTxt.text = "" + counter;
	}
}
