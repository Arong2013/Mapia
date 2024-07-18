using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //해당 플레이어에게 특정 상태를 검
            //움직일 수 없으며 5~10초마다 데미지가 10씩 받게됨

            Debug.Log("함정 발동");




        }

    }
}
