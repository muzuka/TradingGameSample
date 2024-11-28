using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiceSelection : MonoBehaviour
{
    public SpiceInventory Inventory;
    public Button Submit;
    public Button Cancel;

    public delegate void SumbitDelegate(SpiceUnit unit);
    public SumbitDelegate OnSubmit;

    List<Toggle> _spiceToggleList;
    SpiceUnit _selectedSpices;
    
    public void Initialize(SpiceUnit unit, SumbitDelegate submit)
    {
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
        for (int i = 0; i < Inventory.GetSpice().Count; i++)
        {
            tog = Inventory.GetSpice()[i].GetComponent<Toggle>();
            if (tog.isOn)
            {
                _selectedSpices.Add(tog.name, 1);
            }
        }
    }
}
