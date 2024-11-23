using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpiceUnit
{
    public int brown;
    public int green;
    public int red;
    public int yellow;

    public override string ToString()
    {
        return $"Yellow: {yellow}, Red: {red}, Green: {green}, Brown: {brown}";
    }
}
