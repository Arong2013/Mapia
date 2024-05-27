using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ETC : MonoBehaviour
{
    public EtcItemData etcItemData;
    int id;
    string name;
    string description;
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        id = etcItemData.ID; name = etcItemData.Name;

        description = etcItemData.Tooltip;

        spriteRenderer.sprite = etcItemData.IconSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //플레이어 인벤토리에 넣어짐
            //해당 오브젝트 제거 
        }
    }


}
