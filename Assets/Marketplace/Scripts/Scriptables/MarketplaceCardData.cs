using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpiceRoadCardData")]
public class MarketplaceCardData : ScriptableObject
{
    public List<PointCard> PointDeck;
    public List<MerchantCard> MerchantDeck;

    public SpiceUnit StartingSpice;
}
