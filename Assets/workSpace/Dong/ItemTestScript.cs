using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemTestScript : MonoBehaviourPunCallbacks
{

    public ItemData itemData;

    PhotonView PV;

    int damage;
    float distance;

    int direction;
    Vector2 StartPos;


    bool Activate = false;

    // Start is called before the first frame update
    void Start()
    {
        PhotonView PV = GetComponent<PhotonView>();
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Activate == true)
        {
            if(itemData.TYPE != ItemType.Consume)
            {
                //클릭한 위치가 현재 플레이어의 위치보다     
                switch (direction)
                {
                    case 1:
                        transform.position = new Vector3(transform.position.x + 10 * Time.deltaTime, transform.position.y);
                        break;

                    case 2:
                        transform.position = new Vector2(transform.position.x - 10 * Time.deltaTime, transform.position.y);
                        break;

                    case 3:
                        transform.position = new Vector2(transform.position.x, transform.position.y + 10 * Time.deltaTime);
                        break;

                    case 4:
                        transform.position = new Vector2(transform.position.x, transform.position.y - 10 * Time.deltaTime);
                        break;

                    default:
                        Debug.LogError("Error");
                        break;
                }

                if (Vector2.Distance(transform.position, StartPos) >= distance)
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
            else
            {
                //소비아이템 사용
            }

        }
        



    }

    public void GetData(ItemData data, int way)
    {
        itemData = data;
        if(data.TYPE != ItemType.Consume)
        {
            damage = data.DAMAGE;
            distance = data.WEAPONDISTANCE;

            direction = way;
        }
        else
        {
            //포션효과 발동
        }
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        SR.sprite = data.IconSprite;
        Activate = true;
    }

}
