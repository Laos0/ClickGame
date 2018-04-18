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

    // How much the price increases when an item is purchased
    public float scaleValue;

    // Attempt to purchase another one of this item
    public void purchase()
    {
        // Check whether the player has enough funds
        if (true)
        {
            // Subtract funds from the player's wallet
            // --TODO--
            // Add one of this item
            numberOwned++;
        }
    }

    // Return the number of items of this type the player owns
    public int getNumberOwned() { return numberOwned; }

    // Increases the cost to purchase one of this item
    // Called every time the player purchases this item
    void increasePrice()
    {
        // Scale the price by a fixed value
        // Value is floored and converted to an int
        price *= (int)Mathf.Floor(1.2f);
    }

    // Returns whether or not the player has enough points to unlock this item in the shop
    bool checkThreshold()
    {
        // Check whether the player has enough points to unlock this item in the shop
        return (Currency.counter >= threshold);
    }

    // Return the number of clicks per tick that this item generates
    public int getClicks() { return clicksPerTick; }

    // Get the price
    public int getPrice()
    {
        return price;
    }
}
