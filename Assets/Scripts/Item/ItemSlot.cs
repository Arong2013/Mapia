using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class ItemSlot : MonoBehaviour
{
    private int count; 
    public Item item;
    public Image myImage;

    public void Start()
    {
        //myImage = GetComponent<Image>();
    }


    public void SetItem(Item data)
    {
        item = data;
        if (item != null)
            myImage.sprite = item.Data.IconSprite;
        else
            myImage.sprite = null;
    }


}
