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
    }
}
