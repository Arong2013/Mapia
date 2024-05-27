using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Potion : MonoBehaviour
{
    public PortionItemData potion;


    public int id;
    public string name;
    public string description;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        id = potion.ID;
        name = potion.Name;
        description = potion.Tooltip;
        spriteRenderer.sprite = potion.IconSprite;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //�÷��̾� �κ��丮�� �־���
            //�ش� ������Ʈ ���� 
        }
    }



}
