using UnityEngine;

public abstract class EquipmentItemData : ItemData
{
    /// <summary> �ִ� ������ </summary>
    public int MaxDurability => _maxDurability;

    [SerializeField] private int _maxDurability = 100;
}

[System.Serializable]
public abstract class EquipmentItem : Item
{
    public EquipmentItemData EquipmentData { get; private set; }

    /// <summary> ���� ������ </summary>
    public int Durability
    {
        get => _durability;
        set
        {
            if (value < 0) value = 0;
            if (value > EquipmentData.MaxDurability)
                value = EquipmentData.MaxDurability;

            _durability = value;
        }
    }
    private int _durability;

    public EquipmentItem(EquipmentItemData data) : base(data)
    {
        EquipmentData = data;
        Durability = data.MaxDurability;
    }

    // Item Data ���� �ʵ尪�� ���� �Ű������� ���� �����ڴ� �߰��� �������� ����
    // �ڽĵ鿡�� ��� �߰������ �ϹǷ� ���������鿡�� ����
}