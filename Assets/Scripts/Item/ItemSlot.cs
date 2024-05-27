using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Inventory[] inventories;
    public int slotCount = 6;
    public ItemData Test;
    // Start is called before the first frame update
    void Start()
    {
        inventories = gameObject.GetComponentsInChildren<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemKeep(ItemData item) //나중에 아이템 데이터 플레이어나 아이템쪽에서 받았을때 사용할 메서드
    {
        for(int i=0; i<slotCount; i++) 
        {
            if (inventories[i].data == null)
            {
                inventories[i].SetData(item);
                return; //아이템을 저장해서 바로 메서드 실행중지
            }
        }
    }

    public void Itemstorage() //테스트용 
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (inventories[i].data == null)
            {
                inventories[i].SetData(Test);
                return; //아이템을 저장해서 메서드 중지
            }

        }

    }

    public void UseItem() //테스트용222
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (inventories[i].data != null)
            {
                //해당 위치의 아이템을 사용하고 특정 효과를 줌
                inventories[i].ResetData();
                return; //아이템을 사용해서 메서드 중지
            }

        }
    }

}
