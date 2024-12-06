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
        MerchantDeck.InitializeMerchantDeck(GameData.MerchantDeck, BuyCard);
        PointDeck.InitializePointDeck(GameData.PointDeck, BuyCard);
        InitializeInventory();
        InitializeHand();
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
        if (!_playerInventory.CanBuy(card.Cost))
        {
            Debug.Log("Cannot buy point card.");
            return;
        }
        
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
}
