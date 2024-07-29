using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[System.Serializable]
public class Inventory :MonoBehaviour
{
    readonly List<Item> items = new List<Item>();
    public List<Item> Items => items;
    public bool isCanAdd
    {
        get
        {
            return 3 > items.Count;
        }
    }
    public bool AddItem(Item _item)
    {
        if (!isCanAdd)
            return false;
        if (_item is CountableItem countItem)
        {
            if (Items.Count > 0)
            {
                for (var i = 0; i < Items.Count; i++)
                {
                    var curitem = Items[i];
                    if (curitem.Data == _item.Data && curitem is CountableItem ci)
                    {
                        countItem.SetAmount(-ci.AddAmountAndGetExcess(countItem.Amount));
                    }
                }
            }
            if (countItem.IsEmpty)
                return true;
        }
        Items.Add(_item);
        return true;
    }
}
