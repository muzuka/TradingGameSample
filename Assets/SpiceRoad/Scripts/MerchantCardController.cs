using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantCardController : MonoBehaviour
{
    public SpiceInventory Inventory;

    MerchantCard _card;

    public void InitializeCard(MerchantCard card)
    {
        _card = card;
        Inventory.AddSpice(card);
    }

    public MerchantCard GetCard()
    {
        return _card;
    }
}
