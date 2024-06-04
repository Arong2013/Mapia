using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class Inventory : MonoBehaviour
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    public Image NowItem_img;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        Transform inventory = UiUtils.GetUI<InventoryUI>().gameObject.transform;
        //UIUtils에서 컴포넌트 가져와서 그걸 컬렉션에 넣어줘야함
        foreach (Transform child in inventory)
        {
            //Debug.Log(child.childCount);
            ItemSlot slot = child.GetComponentInChildren<ItemSlot>();

            if(slot != null)
            {
                itemSlots.Add(slot);
            }

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && PV.IsMine)
        {
            if(UiUtils.GetUI<InventoryUI>().gameObject.activeSelf == false)
            {
                UiUtils.GetUI<InventoryUI>().gameObject.SetActive(true);
            }
            else
            {
                UiUtils.GetUI<InventoryUI>().gameObject.SetActive(false);
            }
            
        }
    }


    //아이템 획득
    public void GetItem(Item item)
    {
        for(int i=0; i<itemSlots.Count; i++)
        {
            if (itemSlots[i].item == null)
            {
                itemSlots[i].SetItem(item);
                return;
            }
            else
            {
                //no storage
            }


        }
        Debug.Log("NoStorage");
    }

    public void UseItem(int num)
    {

        if (itemSlots[num].item != null)
        {
            //아이템 효과 발동을 여기서 하거나 아니면 다른 곳에서
            itemSlots[num].SetItem(null);
            NowItemImage(num);
            return;
        }
        else
        {
            //empty item storage
        }

    }

    public void NowItemImage(int num)
    {
        if (itemSlots[num].item != null)
        {
            NowItem_img.sprite = itemSlots[num].item.Data.IconSprite;
        }
        else
        {
            Debug.Log("nothing");
            NowItem_img.sprite = null;
        }
        
    }


   
}
