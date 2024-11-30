using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MerchantCardController : MonoBehaviour
{
    public SpiceInventory Inventory;

    Button _button;
    MerchantCard _card;

    public void InitializeCard(MerchantCard card, UnityAction action)
    {
        _card = card;
        Inventory.AddSpice(_card);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(action);
    }

    public void SetCard(MerchantCard card)
    {
        Inventory.Clear();
        _card = card;
        Inventory.AddSpice(_card);
    }

    public MerchantCard GetCard()
    {
        return _card;
    }
}
