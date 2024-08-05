using UnityEngine;
public abstract class ItemData : ScriptableObject
{
    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;

    public int MaxAmount => _maxAmount;

    public Sprite IconSprite => _iconSprite;
    [SerializeField] private int _id;
    [SerializeField] private string _name;    // ������ �̸�
    [Multiline]
    [SerializeField] private string _tooltip; // ������ ����
    [SerializeField] private Sprite _iconSprite; // ������ ������
    [SerializeField] private int _maxAmount = 99;
    public abstract Item CreateItem();
}
public abstract class Item
{
    public int Amount { get; protected set; }
    public int MaxAmount => Data.MaxAmount;
    public bool IsMax => Amount >= Data.MaxAmount;
    public bool IsEmpty => Amount <= 0;
    public void SetAmount(int amount)
    {
        Amount = Mathf.Clamp(amount, 0, MaxAmount);
    }
    public int AddAmountAndGetExcess(int amount)
    {
        int nextAmount = Amount + amount;
        SetAmount(nextAmount);

        return (nextAmount > MaxAmount) ? (nextAmount - MaxAmount) : 0;
    }
    public Item SeperateAndClone(int amount)
    {
        // ������ �Ѱ� ������ ���, ���� �Ұ�
        if (Amount <= 1) return null;

        if (amount > Amount - 1)
            amount = Amount - 1;

        Amount -= amount;
        return Clone(amount);
    }
    public ItemData Data { get; private set; }
    public Item(ItemData data) => Data = data;
    protected abstract Item Clone(int amount);
}