using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceRoadManager : MonoBehaviour
{
    public SpiceRoadCardData GameData;

    public MerchantDeckController MerchantDeck;
    public PointDeckController PointDeck;

    public SpiceInventory PlayerInventory;
    const int maxSpice = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        MerchantDeck.InitializeMerchantDeck(GameData.MerchantDeck);
        PointDeck.InitializePointDeck(GameData.PointDeck);
        InitializeInventory();
    }

    void InitializeInventory()
    {
        int leftover = maxSpice - GameData.StartingSpice.totalUnits();

        if (leftover < 0)
            return;
        
        PlayerInventory.AddSpice(GameData.StartingSpice);

        for (int i = 0; i < leftover; i++)
        {
            PlayerInventory.AddEmpty();
        }
    }
}
