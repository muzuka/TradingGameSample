using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PointCardController : MonoBehaviour
{
    public TMP_Text PointText;
    public SpiceInventory Inventory;

    Button _button;
    PointCard _card;

    public void InitializeCard(PointCard card, UnityAction action)
    {
        _card = card;
        Inventory.AddSpice(card.Cost);
        PointText.text = card.Points.ToString();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(action);
    }
    
    public void SetCard(PointCard card)
    {
        Inventory.Clear();
        _card = card;
        Inventory.AddSpice(_card.Cost);
    }
    
    public PointCard GetCard()
    {
        return _card;
    }
}
