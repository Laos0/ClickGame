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

    float shopValue;

    // List of UI elements for the different shop items
    public List<UnityEngine.UI.Button> ShopItemButtons = new List<UnityEngine.UI.Button>();

    // The total number of clicks that occur per tick
    float autoclickValue;

    // The number of shop items the player has unlocked
    // Not sure if I'm gonna use this or some other method, so just wait
    int itemsUnlocked;

    float newButtonX;
    float newButtonY;
    float newButtonY_change;

    // Use this for initialization
    void Start () {
        shopValue = 0;

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
        newButtonY_change = ShopItemButtons[0].GetComponent<RectTransform>().rect.height * 1.5f;
        newButtonY = ShopItemButtons[0].transform.position.y - newButtonY_change;

        // Load the game data from the file
        loadGame(ConfigManager.getConfig());
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

                itemsUnlocked++;

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

        updateButtonText(index);
        // Check whether the player can afford this item
        if (MGM.getCurrency() >= ShopItems[index].getPrice())
        {
            // Subtract this much money from the player's wallet
            MGM.subToCurrency(ShopItems[index].getPrice());
            // Increase the autoclicker value
            autoclickValue += ShopItems[index].getClicks();

            // Add the cost of this item to shopValue
            shopValue += ShopItems[index].getPrice();

            // Call the shopItem's purchase function (for bookkeeping)
            ShopItems[index].purchase();

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

    // Returns the total value of your shop
    public float getShopValue()
    {
        return shopValue;
    }

    // Return the number of the given shop item the player already owns
    public int getNumberOwned(int index)
    {
        return ShopItems[index].getNumberOwned();
    }

    // Return the number of items the player has unlocked
    public int getItemsUnlocked()
    {
        return itemsUnlocked;
    }


    // Load the game data from the given config file
    public void loadGame(Configuration config)
    {
        MainGameManager.clickValue = config["Statistics"]["Click Value"].IntValue;
        Debug.Log("MainGameManager.clickValue");

        // Load the currency
        MGM.addToCurrency(config["Statistics"]["Points"].FloatValue);

        // Load the shop value
        shopValue = config["Statistics"]["Shop Value"].FloatValue;

        // Buy the number of autominer's the player should have
        for (int j = 0; j < config["Items Owned"]["Item0"].IntValue; j++)
        {

            Debug.Log("Bought");
            // Increase the autoclicker value
            autoclickValue += ShopItems[0].getClicks();

            // Call the shopItem's purchase function (for bookkeeping)
            ShopItems[0].purchase();
        }

        updateButtonText(0);

        // Load the shop data
        for (int i = 1; i <= config["Statistics"]["Items Unlocked"].IntValue; i++)
        {   
            Debug.Log(ShopItems[i].getName());
            // Instantiate a new Button
            GameObject newButton = Instantiate(Resources.Load<GameObject>(ShopItems[i].getName()));
            ShopItemButtons.Add(newButton.GetComponent<UnityEngine.UI.Button>());

            // Set the new button to be a child of this button
            newButton.transform.SetParent(this.gameObject.transform);

            // Set this button's position
            newButton.transform.position = new Vector2(newButtonX, newButtonY);
            newButtonY -= newButtonY_change;
            newButton.GetComponent<ShopItemButton>().initialize(this, i);

            // Buy the number of items that this item should have
            for (int j = 0; j < config["Items Owned"]["Item" + i].IntValue; j++)
            {

                Debug.Log("Bought");
                // Increase the autoclicker value
                autoclickValue += ShopItems[i].getClicks();

                // Call the shopItem's purchase function (for bookkeeping)
                ShopItems[i].purchase();
            }

            // Bookkeeping
            itemsUnlocked++;
            updateButtonText(i);
        }


        // Calculate the amount of currency earned since the game was saved

    }
}
