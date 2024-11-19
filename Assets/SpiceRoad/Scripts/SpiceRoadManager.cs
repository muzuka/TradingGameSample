using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceRoadManager : MonoBehaviour
{
    public SpiceRoadCardData GameData;

    public MerchantDeckController MerchantDeck;
    public PointDeckController PointDeck;
    
    // Start is called before the first frame update
    void Start()
    {
        MerchantDeck.InitializeMerchantDeck(GameData.MerchantDeck);
        PointDeck.InitializePointDeck(GameData.PointDeck);
    }

}
