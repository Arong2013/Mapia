using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeItemData : ItemData
{
    [SerializeField] int damage;
    [SerializeField] float distance;

    public CloseRangeItemData(int damage, float distance)
    {
        this.damage = damage;
        this.distance = distance;
    }

    public override Item CreateItem()
    {
        return new CloseRangeItem(this);
    }
}

public class CloseRangeItem : Item
{
    public CloseRangeItem(CloseRangeItemData data) : base(data)
    {
        
    }

    protected override Item Clone(int amount)
    {
        throw new System.NotImplementedException();
    }
}


public class SpearUpgrade : Spear
{
    public SpearUpgrade(CloseRangeItemData data) : base(data)
    {

    }
}



public class Spear : Item
{
    CloseRangeItemData Weapondata;

    public Spear(CloseRangeItemData data) : base(data)
    {
        data = new CloseRangeItemData(10, 5);
        //data.IconSprite = this;
        Weapondata = data;
        

    }

    protected override Item Clone(int amount)
    {
        throw new System.NotImplementedException();
    }

}
