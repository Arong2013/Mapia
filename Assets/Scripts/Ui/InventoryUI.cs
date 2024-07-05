using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class InventoryUI : MonoBehaviour
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    public static int ItemSlotCount = 3;
    public ItemSlot DragSlot;
    //PhotonView PV;

    private void Awake()
    {
        //PV = GetComponent<PhotonView>();
        var inventory = UiUtils.GetUI<InventoryUI>().gameObject.transform;
        //UIUtils에서 컴포넌트 가져와서 그걸 컬렉션에 넣어줘야함
        foreach (Transform child in inventory)
        {
            //Debug.Log(child.childCount);
            ItemSlot slot = child.GetComponentInChildren<ItemSlot>();

            if (slot != null)
            {
                itemSlots.Add(slot);
            }

        }
    }

    public void SetItem(Item data)
    {
        //item = data;
        //type = item.Data.Itemtype;
        //if (item != null)
        //    myImage.sprite = item.Data.IconSprite;
        //else
        //    myImage.sprite = null;

    }


    //해당 오브젝트 스크립트가 꺼져있기 때문에 다른 스크립트에서 해당 함수를 실행시켜줌
    void ItemSlotIntialize()
    {
        Debug.Log("작동");
        var inventory = UiUtils.GetUI<InventoryUI>().gameObject.transform;
        //UIUtils에서 컴포넌트 가져와서 그걸 컬렉션에 넣어줘야함
        foreach (Transform child in inventory)
        {
            //Debug.Log(child.childCount);
            ItemSlot slot = child.GetComponentInChildren<ItemSlot>();

            if (slot != null)
            {
                itemSlots.Add(slot);
            }

        }
    }


    public bool ItemSlotCheck(Item item, ItemType type)
    {
        ItemSlotIntialize();


        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].type == type && itemSlots[i].item == null)
            {
                Debug.Log("자리 있음");
                itemSlots[i].SetItem(item);
                return true;
                
            }

        }
        Debug.Log("자리 없음");
        return false;

    }



}
