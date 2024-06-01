using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public ItemSlot[] itemSlots;
    public Image NowItem_img;

    private void Start()
    {
        itemSlots = GetComponentsInChildren<ItemSlot>();
    }
    

    //아이템 획득
    public void GetItem(Item item)
    {
        for(int i=0; i<itemSlots.Length; i++)
        {
            if(itemSlots[i].item == null)
            {
                Debug.Log(item.Data.Name);
                itemSlots[i].SetItem(item);
                return;
            }
            else
            {

                //no storage
            }


        }
        
        Debug.Log("Error");
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


    //List<Item> itemList = new List<Item>();
    //public Image NowItem_img;

    //int MaxItemCount =  3;
    //public bool GetItem()
    //{
    //    return false;
    //}
    //public int ItemLeft()
    //{
    //    int num = 0;
    //    int lastItem = 0;

    //    for (int i = 0; i < itemList.Count; i++)
    //    {
    //        if (itemList[i].Data != null)
    //        {
    //            num++;
    //            lastItem = i;
    //        }

    //    }
    //    return num;
    //}

    //public void NumCheck()
    //{
    //    for (int i = 0; i < itemList.Count; i++)
    //    {
    //        if (itemList[i].Data != null)
    //        {

    //            return;
    //        }
    //    }



    //    return;
    //}


    //public void NowItemImage(int num)
    //{
    //    NowItem_img.sprite = itemList[num].Data.IconSprite;
    //}


    //public void GetItem(ItemData data)
    //{
    //    for (int i = 0; i < itemList.Count; i++)
    //    {
    //        if (itemList[i] == null)
    //        {
    //            itemList[i] = data;
    //            itemList[i].image.sprite = data.IconSprite;
    //            return;
    //        }
    //        else
    //        {

    //        }

    //    }
    //}

    //public void UseItem(int num)
    //{

    //    if (itemList[num].data != null)
    //    {
    //        itemList[num].Data = null;
    //        //itemList[num].Data.IconSprite = null;
    //        NowItemImage(num);

    //        return;
    //    }
    //    else
    //    {

    //        Debug.Log("asdf");
    //    }

    //}

}
