using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem {

    // The number of this item the player already owns
    protected int numberOwned;
    // The current price to purchase a new one of this item
    protected int price;
    // The number of times this item clicks every time interval dt
    protected int clicksPerTick;
    // The number of points the player must already have before this item appears in the shop
    protected int threshold;
    // This shop item's UI image
    protected Sprite shopIcon;

    protected string name;

    // How much the price increases when an item is purchased
    public float scaleValue;

    // Attempt to purchase another one of this item
    public void purchase()
    {
        // Increase the number owned by one
        numberOwned++;
        // Increase the price
        increasePrice();

        Debug.Log("New Price: " + price.ToString());
    }

    // Return the number of items of this type the player owns
    public int getNumberOwned() { return numberOwned; }

    // Increases the cost to purchase one of this item
    // Called every time the player purchases this item
    void increasePrice()
    {
        // Scale the price by a fixed value
        // Value is floored and converted to an int
        price = (int)Mathf.Floor(1.2f * price);
    }

    // Returns whether or not the player has enough points to unlock this item in the shop
    public bool passedThreshold()
    {
        // Check whether the player has enough points to unlock this item in the shop
        return (Currency.counter >= threshold);
    }

    // Return the number of clicks per tick that this item generates
    public int getClicks() { return clicksPerTick; }

    // Get the price
    public int getPrice() { return price; }

    // Get the number of this item the player owns
    public int getCount () { return numberOwned; }

    // Get the item's name
    public string getName () { return name; }

    
}
