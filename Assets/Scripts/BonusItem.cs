using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusItem : Item
{

    public int points;

    public BonusItem(string name, float weight, Image icon, int points) : base(name, weight)
    {
        this.points = points;
    }

}
