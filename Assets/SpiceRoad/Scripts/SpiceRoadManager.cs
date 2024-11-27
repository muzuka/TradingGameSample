using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles game logic and updating UI
 */
public class SpiceRoadManager : MonoBehaviour
{
    public SpiceRoadCardData GameData;

    [Header("UI connections")]
    public MerchantDeckController MerchantDeck;
    public PointDeckController PointDeck;
    public HandController Hand;
    public SpiceInventory InventoryCard;
    public Transform PopUpParent;

    [Header("Pop up")] 
    public SpiceSelection SelectionMenu;
    
    const int _maxSpice = 10;

    SpiceUnit _playerInventory;
    MerchantCard _targetCard;
    
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
        int leftover = _maxSpice - GameData.StartingSpice.TotalUnits();

        if (leftover < 0)
            return;
        
        InventoryCard.AddSpice(GameData.StartingSpice);
        _playerInventory = GameData.StartingSpice;

        for (int i = 0; i < leftover; i++)
        {
            InventoryCard.AddEmpty();
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
                if (_playerInventory.CanBuy(card.Cost))
                {
                    _playerInventory.Trade(card.Cost, card.Reward);
                }
                break;
            case Enums.MerchantType.ADD:
                InventoryCard.AddSpice(card);
                break;
            case Enums.MerchantType.UPGRADE:
                _targetCard = card;
                SpiceSelection obj = Instantiate(SelectionMenu.gameObject, PopUpParent).GetComponent<SpiceSelection>();
                obj.Initialize(_playerInventory, FinishBuy);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void BuyCard(MerchantCard card)
    {
        _targetCard = card;
        SpiceSelection obj = Instantiate(SelectionMenu.gameObject, PopUpParent).GetComponent<SpiceSelection>();
        obj.Initialize(_playerInventory, FinishUpgrade);
    }

    void BuyCard(PointCard card)
    {
        
    }

    void FinishUpgrade(SpiceUnit unit)
    {
        _playerInventory.Upgrade(unit);
    }

    void FinishBuy(SpiceUnit unit)
    {
        _playerInventory.Subtract(unit);
    }
}
