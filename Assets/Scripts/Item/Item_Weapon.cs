using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : MonoBehaviour
{
    public WeaponItemData weaponItemData;
    int id;
    string name;
    string description;
    SpriteRenderer spriteRenderer;
    int Durability;
    int Attack;
    float AtkCoolTime;
    float Distance;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        id = weaponItemData.ID; name = weaponItemData.Name;

        description = weaponItemData.Tooltip;

        spriteRenderer.sprite = weaponItemData.IconSprite;

        Durability = weaponItemData.MaxDurability;

        Attack = weaponItemData.ATK; AtkCoolTime = weaponItemData.AttackCoolTime; Distance = weaponItemData.Distance;
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
