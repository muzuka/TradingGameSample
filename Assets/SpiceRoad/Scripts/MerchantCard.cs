using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[System.Serializable]
public class MerchantCard
{
    public Enums.MerchantType Type;
    public SpiceUnit Cost;
    public SpiceUnit Reward;

    public MerchantCard(Enums.MerchantType type, SpiceUnit cost, SpiceUnit reward)
    {
        Type = type;
        Cost = cost;
        Reward = reward;
    }
}
