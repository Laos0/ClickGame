using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicksDebuggingHelp : MonoBehaviour {

    public UnityEngine.UI.Text moneyDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moneyDisplay.text = Currency.counter.ToString();
	}
}
