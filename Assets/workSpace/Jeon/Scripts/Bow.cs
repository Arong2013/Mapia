using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item
{
    public Bow(ItemData data) : base(data)
    {

    }
    protected override Item Clone(int amount)
    {
        return new Bow(Data);
    }
}
public class RangeItemData : ItemData
{
    [SerializeField] int attack;
    [SerializeField] float distance;
    public override Item CreateItem()
    {
        return new RangeItem(this);
    }
}
public class RangeItem : Item
{
    public RangeItem(RangeItemData data) : base(data){}

    protected override Item Clone(int amount)
    {
        return new RangeItem(Data as RangeItemData);
    }
}