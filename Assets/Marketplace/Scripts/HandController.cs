using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public GameObject MerchantCardPrefab;
    public List<MerchantCardController> Cards;

    public void InitializeHand()
    {
        Cards = new List<MerchantCardController>();
    }
    
    public void AddCard(MerchantCard card, UnityAction buttonAction)
    {
        GameObject obj = Instantiate(MerchantCardPrefab, transform);
        obj.GetComponent<MerchantCardController>().InitializeCard(card, buttonAction);
        Cards.Add(obj.GetComponent<MerchantCardController>());
    }

    public void Discard(MerchantCard card)
    {
        MerchantCardController control = GetCardController(card);

        if (control == null)
        {
            Debug.LogWarning("Error: Couldn't find card in hand.");
            return;
        }
        
        control.Discard();
    }

    public void Refresh()
    {
        Cards.ForEach((x) =>
        {
            if (x.isDiscarded())
            {
                x.Refresh();
            }
        });
    }

    MerchantCardController GetCardController(MerchantCard card)
    {
        return Cards.Find((x) => x.GetCard().IsEqual(card));
    }
}
