using UnityEngine;
using UnityEngine.Serialization;

/*
 * Represents a collection of spices e.g. inventories, card costs, etc...
 */
[System.Serializable]
public class SpiceUnit
{
    [FormerlySerializedAs("brown")] public int Brown;
    [FormerlySerializedAs("green")] public int Green;
    [FormerlySerializedAs("red")] public int Red;
    [FormerlySerializedAs("yellow")] public int Yellow;

    public SpiceUnit(int b, int g, int r, int y)
    {
        Brown = b;
        Green = g;
        Red = r;
        Yellow = y;
    }

    public void Add(SpiceUnit unit)
    {
        Brown += unit.Brown;
        Green += unit.Green;
        Red += unit.Red;
        Yellow += unit.Yellow;
    }
    
    public void Subtract(SpiceUnit unit)
    {
        Brown = Mathf.Clamp(Brown - unit.Brown, 0, Brown);
        Green = Mathf.Clamp(Green - unit.Green, 0, Green);
        Red = Mathf.Clamp(Red - unit.Red, 0, Red);
        Yellow = Mathf.Clamp(Yellow - unit.Yellow, 0, Yellow);
    }

    public bool CanBuy(SpiceUnit cost)
    {
        return (cost.Brown < Brown && 
                cost.Green < Green && 
                cost.Red < Red && 
                cost.Yellow < Yellow);
    }

    public void Trade(SpiceUnit cost, SpiceUnit reward)
    {
        if (!CanBuy(cost))
            return;
        
        Subtract(cost);
        Add(reward);
    }

    public void Upgrade(SpiceUnit unit)
    {
        
    }

    public bool IsEqual(SpiceUnit query)
    {
        return (Brown == query.Brown &&
                Green == query.Green &&
                Red == query.Red &&
                Yellow == query.Yellow);
    }
    
    public int TotalUnits()
    {
        return Brown + Green + Red + Yellow;
    }
    
    public override string ToString()
    {
        return $"Yellow: {Yellow}, Red: {Red}, Green: {Green}, Brown: {Brown}";
    }
}
