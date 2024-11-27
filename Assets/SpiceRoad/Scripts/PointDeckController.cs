using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PointDeckController : MonoBehaviour
{
    public List<PointCardController> Cards;

    List<PointCard> _pointCards;
    List<PointCard> _discardPile;
    
    public delegate void BuyCardDelegate(PointCard card);
    public BuyCardDelegate BuyCard;
    
    // Start is called before the first frame update
    void Start()
    {
        _pointCards = new List<PointCard>();
        _discardPile = new List<PointCard>();
    }
    
    public void InitializePointDeck(List<PointCard> cardList, BuyCardDelegate cardAction)
    {
        BuyCard += cardAction;
        _pointCards = cardList;
        _discardPile = new List<PointCard>();
        InitializeCards();
    }
    
    void InitializeCards()
    {
        foreach (PointCardController card in Cards)
        {
            card.InitializeCard(DrawCard());
        }
    }

    PointCard DrawCard()
    {
        if (_pointCards == null || _pointCards.Count == 0)
        {
            Debug.LogError("Cannot draw Merchant Card from empty/null deck.");
            return null;
        }
        
        PointCard card = _pointCards[_pointCards.Count - 1];
        
        _discardPile.Add(card);
        _pointCards.Remove(card);

        return card;
    }
}
