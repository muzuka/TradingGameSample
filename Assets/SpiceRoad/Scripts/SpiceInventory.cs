using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class SpiceInventory : MonoBehaviour
{
    public GameObject SpiceSlot;
    public Sprite Arrow;
    
    Color _yellow = new Color(255, 225, 0);
    Color _red = new Color(255, 30, 0);
    Color _green = new Color(105, 211, 0);
    Color _brown = new Color(148, 74, 45);
    Color _grey = new Color(211, 191, 185);

    public void AddSpice(MerchantCard card)
    {
        if (card.Type == Enums.MerchantType.UPGRADE)
        {
            AddImage(Arrow);

            for (int i = 0; i < card.Reward.yellow; i++)
            {
                AddSingle(_grey);
            }
        }
        else if (card.Type == Enums.MerchantType.TRADE)
        {
            AddSpice(card.Cost);
            AddFlippedImaged(Arrow);
            AddSpice(card.Reward);
        }
        else
        {
            AddSpice(card.Reward);
        }
    }
    
    public void AddSpice(SpiceUnit unit)
    {
        for (int i = 0; i < unit.yellow; i++)
        {
            AddSingle(_yellow);
        }
        
        for (int i = 0; i < unit.red; i++)
        {
            AddSingle(_red);
        }
        
        for (int i = 0; i < unit.green; i++)
        {
            AddSingle(_green);
        }
        
        for (int i = 0; i < unit.brown; i++)
        {
            AddSingle(_brown);
        }
    }

    void AddSingle(Color c)
    {
        GameObject obj = Instantiate(SpiceSlot, gameObject.transform);
        obj.GetComponent<Image>().color = c;
        Debug.Log($"{obj.name} Changing color to {c}");
        obj.SetActive(true);
    }

    void AddImage(Sprite img)
    {
        GameObject obj = Instantiate(SpiceSlot, gameObject.transform);
        obj.GetComponent<Image>().sprite = img;
        obj.SetActive(true);
    }

    void AddFlippedImaged(Sprite img)
    {
        GameObject obj = Instantiate(SpiceSlot, gameObject.transform);
        obj.GetComponent<Image>().sprite = img;
        obj.transform.Rotate(0f, 0f, 180f);
        obj.SetActive(true);
    }

    public void Clear()
    {
        Image[] imgs = GetComponentsInChildren<Image>();

        for (int i = 0; i < imgs.Length; i++)
        {
            Destroy(imgs[i].gameObject);
        }
    }
}
