using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Armor,
    QuestItem,
    // ��� �ʿ��� ������ ������ �߰��� �� �ֽ��ϴ�.
}


public abstract class ItemData : ScriptableObject
{
    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;
    public ItemType Itemtype => _type;

    //public ItemObject DropItemPrefab => _dropItemPrefab;
    [SerializeField] private ItemType _type;
    [SerializeField] private int _id;
    [SerializeField] private string _name;    // ������ �̸�
    [Multiline]
    [SerializeField] private string _tooltip; // ������ ����
    [SerializeField] private Sprite _iconSprite; // ������ ������
    
   // [SerializeField] private ItemObject _dropItemPrefab; // �ٴڿ� ������ �� ������ ������

    /// <summary> Ÿ�Կ� �´� ���ο� ������ ���� </summary>
    public abstract Item CreateItem();
}
public abstract class Item
{
    public ItemData Data { get; private set; }
    public Item(ItemData data) => Data = data;
}