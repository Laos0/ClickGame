using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives : ShopItem {
    // Constructor
    public Explosives()
    {
        // Initialize member variables
        numberOwned = 0;
        price = 5000;
        clicksPerTick = 500;
        name = "Explosives";
        threshold = 3000;
    }
}
