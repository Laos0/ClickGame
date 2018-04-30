using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicksDebuggingHelp : MonoBehaviour {

    public UnityEngine.UI.Text moneyDisplay;

    public GameObject MGM_Object;

    public GameObject shopObject;

	// Use this for initialization
	void Start () {
        // Load the config file
        ConfigManager.loadConfig();

        MGM_Object.GetComponent<MainGameManager>().addToCurrency(ConfigManager.getPoints());
	}
	
	// Update is called once per frame
	void Update () {
        moneyDisplay.text = MGM_Object.GetComponent<MainGameManager>().getCurrency().ToString();

        // MGM_Object.GetComponent<MainGameManager>().addToCurrency(shopObject.GetComponent<Shop>().getScaledClicks(Time.deltaTime));
	}
}
