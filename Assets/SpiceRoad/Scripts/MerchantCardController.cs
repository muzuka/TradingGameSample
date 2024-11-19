using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantCardController : MonoBehaviour
{
    public SpiceInventory Inventory;

    MerchantCard _card;

    public void InitializeCard(MerchantCard card)
    {
        Inventory.AddSpice(card);
    }
}
