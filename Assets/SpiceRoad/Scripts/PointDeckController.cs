using System.Collections.Generic;
using UnityEngine;

/*
 * Manages point card collection
 *
 * Replacement cards are added from the rightmost side
 *
 */
public class PointDeckController : MonoBehaviour
{
    public List<PointCardController> Cards;
    
    int _targetPos;
    List<PointCard> _pointCards;
    
    public delegate void BuyCardDelegate(PointCard card);
    public BuyCardDelegate BuyCard;
    
    // Start is called before the first frame update
    void Start()
    {
        _pointCards = new List<PointCard>();
    }
    
    public void InitializePointDeck(List<PointCard> cardList, BuyCardDelegate cardAction)
    {
        BuyCard += cardAction;
        _pointCards = new List<PointCard>(cardList);
        InitializeCards();
    }
    
    void InitializeCards()
    {
        foreach (PointCardController card in Cards)
        {
            card.InitializeCard(DrawCard(), () => { BuyCard(card.GetCard()); });
        }
    }
    
    public void TakeCard(PointCard card)
    {
        _targetPos = -1;
        
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].GetCard().IsEqual(card))
            {
                _targetPos = i;
                break;
            }
        }

        if (_targetPos == -1)
        {
            Debug.LogWarning("Warning: Tried taking point card outside range.");
            return;
        }

        for (int i = _targetPos; i < Cards.Count - 1; i++)
        {
            Cards[i].SetCard(Cards[i+1].GetCard());
        }
        
        Cards[^1].SetCard(DrawCard());
    }

    PointCard DrawCard()
    {
        if (_pointCards == null)
        {
            Debug.LogError("Cannot draw Merchant Card from null deck.");
            return null;
        }

        if (_pointCards.Count == 0)
        {
            Debug.Log("Ran out of cards!");
            return null;
        }
        
        PointCard card = _pointCards[0];
        _pointCards.RemoveAt(0);

        return card;
    }
}
