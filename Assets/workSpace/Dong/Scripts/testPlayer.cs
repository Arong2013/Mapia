using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class testPlayer : MonoBehaviour
{

    public ItemData item;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 5 * Time.deltaTime, transform.position.y);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 5 * Time.deltaTime, transform.position.y);
        }
    
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //UseItem();
        }




    
    }

    public void UseItem()
    {
        ////�������� ����ؿ� 

        ////���Կ� �ִ� �����͸� ������
        //GameObject spear = Instantiate(new GameObject("item1"), transform);
        //SpriteRenderer sR = spear.AddComponent<SpriteRenderer>();
        //sR.sprite = item.IconSprite;        testItemUse tIU = spear.AddComponent<testItemUse>();
        ////tIU.ItemMoving(1);

    }


}
