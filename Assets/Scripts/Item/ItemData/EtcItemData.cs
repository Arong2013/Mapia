using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_ETC_", menuName = "Inventory System/Item Data/ETC", order = 4)]
public class EtcItemData : CountableItemData
{
    public override Item CreateItem()
    {
        return new EtcItem(this);
    }
}
[System.Serializable]
public class EtcItem : CountableItem
{
    public EtcItem(EtcItemData data, int amount = 1) : base(data, amount) { }
    protected override CountableItem Clone(int amount)
    {
       
        return new EtcItem(CountableData as EtcItemData, amount);
    }
}