using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Manages merchant card collection
 * 
 * Players need to pay for cards passed the leftmost
 * Replacement cards are added from the rightmost side
 * 
 */
public class MerchantDeckController : MonoBehaviour
{
    public List<MerchantCardController> Cards;

    int _targetPos;
    List<MerchantCard> _merchantCards;

    public delegate void BuyCardDelegate(MerchantCard card, int cost);
    public BuyCardDelegate BuyCard;
    
    // Start is called before the first frame update
    void Start()
    {
        _merchantCards = new List<MerchantCard>();
    }

    public void InitializeMerchantDeck(List<MerchantCard> cardList, BuyCardDelegate cardAction)
    {
        BuyCard += cardAction;
        _merchantCards = new List<MerchantCard>(cardList);
        InitializeCards();
    }

    void InitializeCards()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            MerchantCard card = DrawCard();
            int j = i;
            Cards[i].InitializeCard(card, () => { BuyCard(card, j); });
        }
    }

    public void TakeCard()
    {
        if (_targetPos < 0 || _targetPos >= Cards.Count)
        {
            Debug.LogWarning("Warning: Tried taking merchant card outside range.");
            return;
        }

        for (int i = _targetPos; i < Cards.Count - 1; i++)
        {
            Cards[i].SetCard(Cards[i+1].GetCard());
        }
        
        Cards[^1].SetCard(DrawCard());
    }

    MerchantCard DrawCard()
    {
        if (_merchantCards == null)
        {
            Debug.LogError("Error: Null merchant deck");
            return null;
        }
        
        if (_merchantCards.Count == 0)
        {
            Debug.Log("Ran out of cards!");
            return null;
        }
        
        MerchantCard card = _merchantCards[0];
        _merchantCards.RemoveAt(0);

        return card;
    }

    public void SetTarget(int value)
    {
        _targetPos = value;
    }
}
