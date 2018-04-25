using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicksDebuggingHelp : MonoBehaviour {

    public UnityEngine.UI.Text moneyDisplay;

    public GameObject MGM_Object;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moneyDisplay.text = MGM_Object.GetComponent<MainGameManager>().getCurrency().ToString();
	}
}
