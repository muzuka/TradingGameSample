using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointCard
{
    public int Points;
    public SpiceUnit Cost;

    public bool IsEqual(PointCard card)
    {
        return (card.Points == Points) && Cost.IsEqual(card.Cost);
    }
}
