using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Currency : MonoBehaviour {

	
    // UI text object to display the player's currency
	public Text scoreTxt;
    // Amount of currency the player has
	public int counter;

    // Use this for initialization
    void Start () {
        // Initialize the player's currency to zero
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreTxt.text = "" + counter;
	}


    // Give the player more money
    public void addMoney(int amount)
    {
        counter += amount;
    }

    // Attempt to spend amount money
    // Returns true if the player had enough money for the transaction
    public bool spendMoney(int amount)
    {
        // If player has enough money
        if (counter >= amount)
        {
            // Take the player's money
            counter -= amount;
            return true;
        }

        // Return false if they couldn't afford it
        return false;
    }


    // Checks whether the player has at least amount money, without making the player spend it
    public bool hasAtLeast(int amount)
    {
        // If player has enough money
        if (counter >= amount)
        {
            return true;
        }

        // Return false if they don't have enough
        return false;
    }

}
