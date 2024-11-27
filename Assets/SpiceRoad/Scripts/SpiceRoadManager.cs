using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpiceRoadManager : MonoBehaviour
{
    public SpiceRoadCardData GameData;

    [Header("UI connections")]
    public MerchantDeckController MerchantDeck;
    public PointDeckController PointDeck;
    public HandController Hand;
    public SpiceInventory PlayerInventory;
    
    [Header("Pop up")]
    
    const int maxSpice = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        MerchantDeck.InitializeMerchantDeck(GameData.MerchantDeck, BuyCard);
        PointDeck.InitializePointDeck(GameData.PointDeck, BuyCard);
        InitializeInventory();
        InitializeHand();
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

    void InitializeHand()
    {
        var hand = StartingHand();
        
        Hand.InitializeHand();
        Hand.AddCard(hand[0], () => { PlayCard(hand[0]); });
        Hand.AddCard(hand[1], () => { PlayCard(hand[1]); });
    }

    List<MerchantCard> StartingHand()
    {
        var hand = new List<MerchantCard>();
        
        MerchantCard a = new MerchantCard(Enums.MerchantType.ADD, null, 
            new SpiceUnit(0, 0, 0, 2));
        MerchantCard b = new MerchantCard(Enums.MerchantType.UPGRADE, 
            new SpiceUnit(0, 0, 0, 2),
            new SpiceUnit(0, 0, 0, 2));
        hand.Add(a);
        hand.Add(b);
        return hand;
    }

    void PlayCard(MerchantCard card)
    {
        switch (card.Type)
        {
            case Enums.MerchantType.TRADE:
                break;
            case Enums.MerchantType.ADD:
                break;
            case Enums.MerchantType.UPGRADE:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void BuyCard(MerchantCard card)
    {
        
    }

    void BuyCard(PointCard card)
    {
        
    }
}
