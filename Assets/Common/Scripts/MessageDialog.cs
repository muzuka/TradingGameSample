using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    public TMP_Text Description;
    public Button Submit;
    
    public delegate void SumbitDelegate();
    public SumbitDelegate OnSubmit;
    
    public void Initialize(string message)
    {
        Description.text = message;
        Submit.onClick.AddListener(() =>
        {
            OnSelection();
        });
    }

    void OnSelection()
    {
        Destroy(gameObject);
    }
}
