using System;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text PointsText;

    [Header("Pop up")] 
    public SpiceSelection SelectionMenu;
    
    const int _maxSpice = 10;

    SpiceUnit _playerInventory;
    MerchantCard _targetCard;
    int _playerPoints = 0;
    
    void Start()
    {
        InitializeInventory();
        InitializationCheck();
        InitializeHand();
        
        MerchantDeck.InitializeMerchantDeck(GameData.MerchantDeck, BuyCard);
        PointDeck.InitializePointDeck(GameData.PointDeck, BuyCard);
        PointsText.text = "Points: " + _playerPoints;
    }
    
    // Initialization

    void InitializeInventory()
    {
        _playerInventory = new SpiceUnit();
        _playerInventory.Add(GameData.StartingSpice);
        SetInventory();
    }

    void SetInventory()
    {
        int leftover = _maxSpice - _playerInventory.TotalUnits();
        
        InventoryCard.Clear();
        InventoryCard.AddSpice(_playerInventory);
        
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
        
        MerchantCard a = new MerchantCard(Enums.MerchantType.ADD, new SpiceUnit(), 
            new SpiceUnit(0, 0, 0, 2));
        MerchantCard b = new MerchantCard(Enums.MerchantType.UPGRADE, 
            new SpiceUnit(0, 0, 0, 2),
            new SpiceUnit(0, 0, 0, 2));
        hand.Add(a);
        hand.Add(b);
        return hand;
    }

    // User interaction functions

    public void RefreshHand()
    {
        Hand.Refresh();
    }
    
    void PlayCard(MerchantCard card)
    {
        switch (card.Type)
        {
            case Enums.MerchantType.TRADE:
                if (_playerInventory.CanBuy(card.Cost))
                {
                    _playerInventory.Trade(card.Cost, card.Reward);
                    SetInventory();
                    Hand.Discard(card);
                }
                break;
            case Enums.MerchantType.ADD:
                _playerInventory.Add(card.Reward);
                SetInventory();
                Hand.Discard(card);
                break;
            case Enums.MerchantType.UPGRADE:
                _targetCard = card;
                SpiceSelection obj = Instantiate(SelectionMenu.gameObject, PopUpParent).GetComponent<SpiceSelection>();
                obj.Initialize(_playerInventory, _targetCard.Cost.Yellow, FinishUpgrade);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void BuyCard(MerchantCard card, int cost)
    {
        MerchantDeck.SetTarget(cost);
        _targetCard = card;
        
        if (cost == 0)
        {
            FinishBuy(new SpiceUnit());
            return;
        }
        
        SpiceSelection obj = Instantiate(SelectionMenu.gameObject, PopUpParent).GetComponent<SpiceSelection>();
        obj.Initialize(_playerInventory, cost, FinishBuy);
    }

    void BuyCard(PointCard card)
    {
        _playerInventory.Subtract(card.Cost);
        _playerPoints += card.Points;
        PointsText.text = "PlayerPoints: " + _playerPoints;
        PointDeck.TakeCard(card);
        SetInventory();
    }

    // Called by selection screen
    
    void FinishUpgrade(SpiceUnit unit)
    {
        _playerInventory.Upgrade(unit);
        SetInventory();
        Hand.Discard(_targetCard);
    }

    void FinishBuy(SpiceUnit unit)
    {
        _playerInventory.Subtract(unit);
        MerchantDeck.TakeCard();
        Hand.AddCard(_targetCard, () => { PlayCard(_targetCard); });
        SetInventory();
    }

    void InitializationCheck()
    {
        if (GameData == null) { Debug.LogError("Error: Gamedata is null."); }
        else
        {
            if (GameData.PointDeck.Count == 0 || GameData.MerchantDeck.Count == 0)
            {
                Debug.LogError("Error: GameData is missing a deck.");
            }
        }
        
        if (MerchantDeck == null) { Debug.LogError("Error: Merchant Deck is not set."); }
        else
        {
            if (MerchantDeck.Cards == null || MerchantDeck.Cards.Count == 0)
            {
                Debug.LogError("Error: Merchant Deck contents are null");
            }
        }

        if (PointDeck == null) { Debug.LogError("Error: Points Deck is not set."); }
        else
        {
            if (PointDeck.Cards == null || PointDeck.Cards.Count == 0)
            {
                Debug.LogError("Error: Point Deck contents are null");
            }
        }
        
        if (PointsText == null) { Debug.LogError("Error: Points Text is null."); }
        if (Hand == null) { Debug.LogError("Error: Hand is null."); }
        else
        {
            if (Hand.MerchantCardPrefab == null)
            {
                Debug.LogError("Error: Hand card prefab is null.");
            }
        }
        
        if (SelectionMenu == null) { Debug.LogError("Error: Selection menu is not set."); }
        if (InventoryCard == null) { Debug.LogError("Error: Inventory Card is not set."); }
        else
        {
            if (InventoryCard.SpiceColors == null || InventoryCard.SpiceSlot == null)
            {
                Debug.LogError("Error: Player inventory is missing data.");
            }
        }
        
        if (PopUpParent == null) { Debug.LogError("Error: Popup parent is not set."); }
    }
}
