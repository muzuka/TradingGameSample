using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointCardController : MonoBehaviour
{
    public TMP_Text PointText;
    public SpiceInventory Inventory;

    PointCard _card;

    public void InitializeCard(PointCard card)
    {
        Inventory.AddSpice(card.Cost);
        PointText.text = card.Points.ToString();
    }
}
