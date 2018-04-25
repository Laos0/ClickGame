using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;

public class Shop : MonoBehaviour {

    // List of different shop items in the game
    List<ShopItem> ShopItems = new List<ShopItem>();
    // Reference to the MGM game object
    public GameObject MGM_Object;

    // MGM class
    MainGameManager MGM;

    // List of UI elements for the different shop items
    public List<UnityEngine.UI.Button> ShopItemButtons = new List<UnityEngine.UI.Button>();

    // The total number of clicks that occur per tick
    int autoclickValue;

    // The number of shop items the player has unlocked
    // Not sure if I'm gonna use this or some other method, so just wait
    int itemsUnlocked;

    float newButtonX;
    float newButtonY;
    float newButtonY_change;

	// Use this for initialization
	void Start () {
        ConfigManager.loadConfig();

        // Get MGM Reference
        MGM = MGM_Object.GetComponent<MainGameManager>();

        // AutoMiner
        ShopItems.Add(new AutoMiner());
        ShopItems[0].setMGM(MGM);
        ShopItemButtons[0].GetComponent<ShopItemButton>().initialize(this, 0);
        // IndustrialDrill
        ShopItems.Add(new IndustrialDrill());
        ShopItems[1].setMGM(MGM);

        // Explosives
        ShopItems.Add(new Explosives());
        ShopItems[2].setMGM(MGM);

        // Initialize 
        updateButtonText(0);

        newButtonX = ShopItemButtons[0].transform.position.x;
        newButtonY_change = 75;
        newButtonY = ShopItemButtons[0].transform.position.y - newButtonY_change;

        // Load the file
        for (int i = 1; i < ConfigManager.getItems(); i++)
        {
            // Instantiate a new Button
            GameObject newButton = Instantiate(Resources.Load<GameObject>(ShopItems[i].getName()));
            ShopItemButtons.Add(newButton.GetComponent<UnityEngine.UI.Button>());

            // Set the new button to be a child of this button
            newButton.transform.SetParent(this.gameObject.transform);

            newButton.transform.position = new Vector2(newButtonX, newButtonY);
            newButtonY -= newButtonY_change;
            newButton.GetComponent<ShopItemButton>().initialize(this, i);


            // Set the number of items owned based on the stored value
            for (int j = 0; j < ConfigManager.getItemsOwned(i); j++)
            {
                ShopItems[i].purchase();
            }

            updateButtonText(i);
        }
    }
	
	// Update is called once per frame
	void Update () {
		// ShopItemButtons.Count is the number of items the player has already found

        // Loop through every shop item that hasn't been added to the shop yet
        for (int i = ShopItemButtons.Count; i < ShopItems.Count; i++)
        {
            // Check whether the player has passed this item's threshold
            if (ShopItems[i].passedThreshold())
            {

                // Instantiate a new Button
                GameObject newButton = Instantiate(Resources.Load<GameObject>(ShopItems[i].getName()));
                ShopItemButtons.Add(newButton.GetComponent<UnityEngine.UI.Button>());

                // Set the new button to be a child of this button
                newButton.transform.SetParent(this.gameObject.transform);

                newButton.transform.position = new Vector2(newButtonX, newButtonY);
                newButtonY -= newButtonY_change;
                newButton.GetComponent<ShopItemButton>().initialize(this, i);

                updateButtonText(i);

                // Add this shop item to the game
            }
        }
	}

    // Buy an autominer.  This script will be called by the UI buttons
    public void buyAutominer() {
        // Call the buyItem function
        buyItem(0);
        // Do anything special you need to do for autominers
    }

    // Buy an industrial drill
    public void buyIndustrialDrill()
    {
        buyItem(1);
    }

    // Buy explosives
    public void buyExplosives()
    {
        buyItem(2);
    }

    // The separate buy______ functions will call this one, passing in the index for the number they used
    // This is done in this, unusual way because the functions for the buttons are selected from a dropdown menu
    //          By making each buy______ a separate function, I don't need to memorize which item is associated with which index
    public void buyItem(int index)
    {
        // Check whether the player can afford this item
        if (MGM.getCurrency() >= ShopItems[index].getPrice())
        {
            // Subtract this much money from the player's wallet
            MGM.subToCurrency(ShopItems[index].getPrice());
            // Increase the autoclicker value
            autoclickValue += ShopItems[index].getClicks();
            // Call the shopItem's purchase function (for bookkeeping)
            ShopItems[index].purchase();

            // Update the autominer's shop button
            updateButtonText(index);

            Debug.Log("Bought One");
        }
        // What to do if the player can't afford this item
        else
        {
            //--TODO--
            // Add some sort of soud effect or visual effect to show the player that they can't afford this item
            Debug.Log("Can't afford");
        }

        Debug.Log("Currency: " + MGM.getCurrency().ToString());
    }

    // Cheat function to add money to the player's currency
    public void addMoney()
    {
        // Add the money
        MGM.addToCurrency(100);
    }

    // Add a new item to the shop
    void addShopItem (int index)
    {
        // Update this item's button text
        updateButtonText(index);
    }

    // Update an item's button text
    void updateButtonText(int index)
    {
        ShopItemButtons[index].GetComponentInChildren<UnityEngine.UI.Text>().text = (ShopItems[index].getName() + "- " + ShopItems[index].getCount().ToString() + "\n" + ShopItems[index].getPrice().ToString());
    }

    // Return the number of clicks that the autoclicker would generate in dt time
    public float getScaledClicks(float dt)
    {
        // Return the number of clicks that would happen in that time
        float value = dt * autoclickValue;
        Debug.Log("Value: " + value.ToString());
        return value;

    }

}
