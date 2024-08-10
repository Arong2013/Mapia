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
    //public Action<Item> ItemChoiceActions;
    readonly List<Item> items = new List<Item>();
    public List<Item> Items => items;

    public Item Choice;

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

    public void UseItem(int num)
    {
        Items[num] = null;
        Items.RemoveAt(num);
        ItemChangeActions?.Invoke();
    }

    public void ItemChoiceActions(Item item)
    {
        if (item == null)
        {
            Debug.Log("ㅁㄴㅇㄹ");
        }
        else
        {
            Debug.Log(item.Data.name);
        }
        Choice = item;

        //ItemChoiceActions?.Invoke();
        //UiUtils.GetUI<UnderBarUI>().
    }

}
