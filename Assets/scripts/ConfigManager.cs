using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;
using System.IO;

public class ConfigManager {

    static Configuration config;

    // Called when the game is started.  Loads the config file
    public static void loadConfig()
    {
        // Set up the config variable
        config = new Configuration();

        // If the config file doesn't exist, create a new one
        if (!File.Exists("cfg/config.cfg"))
        {
            Debug.Log("Setting up a default config since no file was found!");
            config["Statistics"]["Points"].IntValue = 0;
            config["Statistics"]["Items Unlocked"].IntValue = 1;

            // Items Owned
            config["Items Owned"]["Item0"].IntValue = 0;
            config["Items Owned"]["Item1"].IntValue = 0;
            config["Items Owned"]["Item2"].IntValue = 0;


            saveFile();
        }
        else
        {
            // Load the config from the file
            config = Configuration.LoadFromFile("cfg/config.cfg");
        }
    }

    // Saves the config file
    public static void saveFile()
    {
        config.SaveToFile("cfg/config.cfg");
    }

    // Return the stored number of points
    public static int getPoints()
    {
        return config["Statistics"]["Points"].IntValue;
    }

    // Return the number of items the player has already unlocked
    public static int getItems()
    {
        Debug.Log("Items: " + config["Statistics"]["Items Unlocked"].IntValue.ToString());
        return config["Statistics"]["Items Unlocked"].IntValue;
    }

    // Return the number of items owned of the type given by the index
    public static int getItemsOwned(int index)
    {
        return config["Items Owned"]["Item"+index.ToString()].IntValue = 0;
    }
}
