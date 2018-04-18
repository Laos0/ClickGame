using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    // List of different shop items in the game
    List<ShopItem> ShopItems = new List<ShopItem>();

    // List of UI elements for the different shop items
    public List<UnityEngine.UI.Button> ShopItemButtons = new List<UnityEngine.UI.Button>();

    // The total number of clicks that occur per tick
    int autoclickValue;

	// Use this for initialization
	void Start () {
        ShopItems.Add(new AutoMiner());
        // ShopItemButtons[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Buy an autominer.  This script will be called by the UI buttons
    public void buyAutominer() {
        ShopItems[0].purchase();
        //ShopItemButtons[0].GetComponent<UnityEngine.UI.Text>().text = "Test";
        if (Currency.spendMoney(ShopItems[0].getPrice()))
        autoclickValue += ShopItems[0].getClicks();
    }

}
