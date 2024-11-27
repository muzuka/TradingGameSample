using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

/*
 * UI controller for displaying spice
 */
public class SpiceInventory : MonoBehaviour
{
    public ImageData SpiceColors;
    public GameObject SpiceSlot;

    List<GameObject> _spices;

    void Awake()
    {
        _spices = new List<GameObject>();
    }

    public void AddSpice(MerchantCard card)
    {
        if (card.Type == Enums.MerchantType.UPGRADE)
        {
            AddImage("Arrow");

            for (int i = 0; i < card.Reward.Yellow; i++)
            {
                AddImage("Grey");
            }
        }
        else if (card.Type == Enums.MerchantType.TRADE)
        {
            AddSpice(card.Cost);
            AddFlippedImaged("Arrow");
            AddSpice(card.Reward);
        }
        else
        {
            AddSpice(card.Reward);
        }
    }
    
    public void AddSpice(SpiceUnit unit)
    {
        for (int i = 0; i < unit.Yellow; i++)
        {
            AddImage("Yellow");
        }
        
        for (int i = 0; i < unit.Red; i++)
        {
            AddImage("Red");
        }
        
        for (int i = 0; i < unit.Green; i++)
        {
            AddImage("Green");
        }
        
        for (int i = 0; i < unit.Brown; i++)
        {
            AddImage("Brown");
        }
    }

    void AddImage(string img)
    {
        GameObject obj = Instantiate(SpiceSlot, gameObject.transform);
        obj.GetComponent<Image>().sprite = SpiceColors.GetSprite(img);
        obj.name = img;
        obj.SetActive(true);
        _spices.Add(obj);
    }

    void AddFlippedImaged(string img)
    {
        GameObject obj = Instantiate(SpiceSlot, gameObject.transform);
        obj.GetComponent<Image>().sprite = SpiceColors.GetSprite(img);
        obj.transform.Rotate(0f, 0f, 180f);
        obj.name = img;
        obj.SetActive(true);
        _spices.Add(obj);
    }

    public void AddEmpty()
    {
        AddImage("Grey");
    }

    public void Clear()
    {
        for (int i = 0; i < _spices.Count; i++)
        {
            Destroy(_spices[i]);
        }
    }

    public List<GameObject> GetSpice()
    {
        return _spices;
    }
}
