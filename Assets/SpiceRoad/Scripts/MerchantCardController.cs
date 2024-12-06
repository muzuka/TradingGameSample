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

    bool _discarded;

    public void InitializeCard(MerchantCard card, UnityAction action)
    {
        _card = card;
        Inventory.AddSpice(_card);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(action);
        _discarded = false;
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

    public bool isDiscarded()
    {
        return _discarded;
    }

    public void Discard()
    {
        Inventory.Clear();
        _button.interactable = false;
    }

    public void Refresh()
    {
        SetCard(_card);
        _button.interactable = true;
    }
}
