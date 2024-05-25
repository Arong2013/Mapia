using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Portion_", menuName = "Inventory System/Item Data/Portion", order = 3)]
public class PortionItemData : CountableItemData
{
    /// <summary> ȿ����(ȸ���� ��) </summary>
    public float Value => _value;
    [SerializeField] private float _value;
    public override Item CreateItem()
    {
        return new PortionItem(this);
    }
}

[System.Serializable]
public class PortionItem : CountableItem
{
    public PortionItem(PortionItemData data, int amount = 1) : base(data, amount) { }

    public bool Use()
    {


        Amount--;

        return true;
    }

    protected override CountableItem Clone(int amount)
    {
        return new PortionItem(CountableData as PortionItemData, amount);
    }
}