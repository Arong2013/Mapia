using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Portion_", menuName = "Inventory System/Item Data/Portion", order = 3)]
public class PortionItemData : ItemData
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
public class PortionItem : Item
{
    public PortionItem(PortionItemData data) : base(data) {Amount = 1;}
    public bool Use()
    {
        Amount--;
        return true;
    }

    protected override Item Clone(int amount)
    {
        return new PortionItem(Data as PortionItemData);
    }
}