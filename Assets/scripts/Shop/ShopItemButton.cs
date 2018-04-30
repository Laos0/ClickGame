using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemButton : MonoBehaviour {

    // Reference to the shop
    Shop shop;

    // ID for this button
    int ID;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Buy this item when the button is clicked
    public void clickButton()
    {
        shop.buyItem(ID);
    }


    // Called when the button is instantiated
    public void initialize(Shop newShop, int newID)
    {
        shop = newShop;
        ID = newID;
        Debug.Log("Initialized");
    }
}
