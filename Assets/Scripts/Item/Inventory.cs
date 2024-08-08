using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

[System.Serializable]
public class Inventory
{
    public Action ItemChangeActions;
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
        if (Items.Count > 0)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var curitem = Items[i];
                if (curitem.Data == _item.Data)
                {
                    _item.SetAmount(-curitem.AddAmountAndGetExcess(_item.Amount));
                }
            }
        }
        if (_item.IsEmpty)
            return true;
        Items.Add(_item);
        ItemChangeActions?.Invoke();
        return true;
    }
}
