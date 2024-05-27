using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Script : MonoBehaviour
{
    public Item item;

    public ItemData ItemData;
    int id;
    string name;
    string description;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        id = ItemData.ID;
        name = ItemData.Name;
        description = ItemData.Tooltip;
        spriteRenderer.sprite = ItemData.IconSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
