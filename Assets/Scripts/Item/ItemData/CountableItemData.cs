using UnityEngine;

public abstract class CountableItemData : ItemData
{
    public int MaxAmount => _maxAmount;
    [SerializeField] private int _maxAmount = 99;
}

[System.Serializable]
public abstract class CountableItem : Item
{
    public CountableItemData CountableData { get; private set; }

    /// <summary> ���� ������ ���� </summary>
    public int Amount { get; protected set; }

    /// <summary> �ϳ��� ������ ���� �� �ִ� �ִ� ����(�⺻ 99) </summary>
    public int MaxAmount => CountableData.MaxAmount;

    /// <summary> ������ ���� á���� ���� </summary>
    public bool IsMax => Amount >= CountableData.MaxAmount;

    /// <summary> ������ ������ ���� </summary>
    public bool IsEmpty => Amount <= 0;


    public CountableItem(CountableItemData data, int amount = 1) : base(data)
    {
        CountableData = data;
        SetAmount(amount);
    }

    /// <summary> ���� ����(���� ����) </summary>
    public void SetAmount(int amount)
    {
        Amount = Mathf.Clamp(amount, 0, MaxAmount);
    }

    /// <summary> ���� �߰� �� �ִ�ġ �ʰ��� ��ȯ(�ʰ��� ���� ��� 0) </summary>
    public int AddAmountAndGetExcess(int amount)
    {
        int nextAmount = Amount + amount;
        SetAmount(nextAmount);

        return (nextAmount > MaxAmount) ? (nextAmount - MaxAmount) : 0;
    }

    /// <summary> ������ ������ ���� </summary>
    public CountableItem SeperateAndClone(int amount)
    {
        // ������ �Ѱ� ������ ���, ���� �Ұ�
        if (Amount <= 1) return null;

        if (amount > Amount - 1)
            amount = Amount - 1;

        Amount -= amount;
        return Clone(amount);
    }

    protected abstract CountableItem Clone(int amount);
}