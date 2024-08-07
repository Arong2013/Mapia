using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeItemData : ItemData
{
    [SerializeField] int damage;
    [SerializeField] float distance;

    public override Item CreateItem()
    {
        return new CloseRangeItem(this);
    }
}

public class CloseRangeItem : Item
{
    public CloseRangeItem(CloseRangeItemData data) : base(data)
    {

    }

    protected override Item Clone(int amount)
    {
        throw new System.NotImplementedException();
    }
}




public class Spear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
