using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpiceRoadCardData")]
public class SpiceRoadCardData : ScriptableObject
{
    public List<PointCard> PointDeck;
    public List<MerchantCard> MerchantDeck;
}
