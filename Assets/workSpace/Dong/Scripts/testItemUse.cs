using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class testItemUse : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemData itemData;
    //GameObject item;
    public GameObject itemasdf;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("Space key pressed");
        //    GameObject myItem = Instantiate(itemasdf, transform);
        //    ItemTestScript ITS = myItem.AddComponent<ItemTestScript>();

        //    ITS.GetData(itemData, 2);

        //    //Spear speardata = item.AddComponent<Spear>();
        //    //SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
        //    //speardata.data = itemData;
        //    //spriteRenderer.sprite = speardata.data.IconSprite;
        //}

        if(Input.GetMouseButtonDown(0))
        {
            UseItem();
        }



    }

    public void UseItem()
    {
        GameObject myItem = Instantiate(itemasdf, transform);
        ItemTestScript ITS = myItem.AddComponent<ItemTestScript>();

        ITS.GetData(itemData, PosCheck());
    }

    
    private int PosCheck()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(mousepos.x) > Mathf.Abs(mousepos.y))
        {
            Debug.Log("x�� y���� Ů�ϴ�. (x: " + mousepos.x + ", y: " + mousepos.y + ")");

            if (mousepos.x > 0)
            {
                //Debug.Log("x�� 0���� Ů�ϴ�. (x: " + mousepos.x + ")");
                return 1;

            }

            // x ��ǥ�� 0���� ������ üũ�մϴ�.
            if (mousepos.x < 0)
            {
                //Debug.Log("x�� 0���� �۽��ϴ�. (x: " + mousepos.x + ")");

                return 2;
            }


        }
        else if (Mathf.Abs(mousepos.y) > Mathf.Abs(mousepos.x))
        {
            Debug.Log("y�� x���� Ů�ϴ�. (x: " + mousepos.x + ", y: " + mousepos.y + ")");

            if (mousepos.y > 0)
            {
                //Debug.Log("y�� 0���� Ů�ϴ�. (y: " + mousepos.y + ")");
                return 3;
            }

            // y ��ǥ�� 0���� ������ üũ�մϴ�.
            if (mousepos.y < 0)
            {
                //Debug.Log("y�� 0���� �۽��ϴ�. (y: " + mousepos.y + ")");
                return 4;
            }

        }

        return 0;
    }

}
