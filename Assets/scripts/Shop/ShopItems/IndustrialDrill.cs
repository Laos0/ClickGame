using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialDrill : ShopItem {
    // Constructor
    public IndustrialDrill()
    {
        // Initialize member variables
        numberOwned = 0;
        price = 300;
        clicksPerTick = 25;
        name = "IndustrialDrill";
        threshold = 220;
    }
}
