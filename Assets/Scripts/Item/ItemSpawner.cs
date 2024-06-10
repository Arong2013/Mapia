using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawner : MonoBehaviour
{
    public PhotonView PV;
    public GameObject DropItem;
    public GameObject[] ItemList;

    float spawnTime =0;
    int num1;
    int num2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime >= 3)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ChooseRandomnum();
                PV.RPC("SpawnItem", RpcTarget.AllBuffered, num1,num2);
            }
            
           // PV.RPC("SpawnItem", RpcTarget.AllBuffered);
           //랜덤이라서 이건 당연히 제대로 작동할 수가 없음 아닌가
        }
    }

    [PunRPC]
    void SpawnItem(int n1,int n2)
    {
       
        Instantiate(ItemList[n1], new Vector3(n2, 2, 0), Quaternion.identity);
        spawnTime = 0;
    }

    [PunRPC]
    void ChooseRandomnum()
    {
        num1 = Random.Range(0, ItemList.Length);
        num2 = Random.Range(0, 10);
    }




}
