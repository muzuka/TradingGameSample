using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MerchantDeckController : MonoBehaviour
{
    public List<MerchantCardController> Cards;

    List<MerchantCard> _merchantCards;
    List<MerchantCard> _discardPile;

    public delegate void BuyCardDelegate(MerchantCard card);
    public BuyCardDelegate BuyCard;
    
    // Start is called before the first frame update
    void Start()
    {
        _merchantCards = new List<MerchantCard>();
        _discardPile = new List<MerchantCard>();
    }

    public void InitializeMerchantDeck(List<MerchantCard> cardList, BuyCardDelegate cardAction)
    {
        BuyCard += cardAction;
        _merchantCards = new List<MerchantCard>(cardList);
        _discardPile = new List<MerchantCard>();
        InitializeCards();
    }

    void InitializeCards()
    {
        foreach (MerchantCardController card in Cards)
        {
            card.InitializeCard(DrawCard());
        }
    }

    public MerchantCard DrawCard()
    {
        if (_merchantCards == null || _merchantCards.Count == 0)
        {
            Debug.LogError("Cannot draw Merchant Card from empty/null deck.");
            return null;
        }
        
        MerchantCard card = _merchantCards[_merchantCards.Count - 1];
        
        _discardPile.Add(card);
        _merchantCards.Remove(card);

        return card;
    }
}
