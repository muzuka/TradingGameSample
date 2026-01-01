using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpiceSelection : MonoBehaviour
{
    public SpiceInventory Inventory;
    public TMP_Text Description;
    public Button Submit;
    public Button Cancel;

    public delegate void SumbitDelegate(SpiceUnit unit);
    public SumbitDelegate OnSubmit;

    int _spiceLimit;
    List<Toggle> _spiceToggleList;
    SpiceUnit _selectedSpices;
    
    public void Initialize(SpiceUnit unit, int limit, SumbitDelegate submit)
    {
        _spiceLimit = limit;
        Description.text = $"Pick {_spiceLimit} spices";
        _selectedSpices = new SpiceUnit();
        Inventory.AddSpice(unit);
        for (int i = 0; i < Inventory.GetSpice().Count; i++)
        {
            Inventory.GetSpice()[i].GetComponent<Toggle>().onValueChanged.AddListener(OnSelection);
        }
        
        OnSubmit += submit;
        Submit.onClick.AddListener(() =>
        {
            OnSubmit(_selectedSpices);
            OnCancel();
        });
        Cancel.onClick.AddListener(OnCancel);
    }

    void OnCancel()
    {
        Destroy(gameObject);
    }

    void OnSelection(bool value)
    {
        _selectedSpices.Reset();
        Toggle tog;
        
        // Count toggles
        for (int i = 0; i < Inventory.GetSpice().Count; i++)
        {
            tog = Inventory.GetSpice()[i].GetComponent<Toggle>();
            if (tog.isOn)
            {
                _selectedSpices.Add(tog.name, 1);
            }
        }
        
        // if selected spices == limit
        // then turn off all unselected toggles
        if (_selectedSpices.TotalUnits() >= _spiceLimit)
        {
            for (int i = 0; i < Inventory.GetSpice().Count; i++)
            {
                tog = Inventory.GetSpice()[i].GetComponent<Toggle>();
                if (!tog.isOn)
                {
                    tog.interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < Inventory.GetSpice().Count; i++)
            {
                tog = Inventory.GetSpice()[i].GetComponent<Toggle>();
                if (!tog.isOn)
                {
                    tog.interactable = true;
                }
            }
        }
    }
}
