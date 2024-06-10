using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Script : MonoBehaviour
{
    public Item item;

    public ItemData ItemData;

    //Item item;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        item = ItemData.CreateItem();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.Data.IconSprite;
    }

    // Update is called once per frame
    void Update()
    {
               
    }     
}
