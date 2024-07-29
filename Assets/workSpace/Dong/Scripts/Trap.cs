using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Trap : MonoBehaviour
{
    PhotonView PV;
    SpriteRenderer spriteRenderer;
    public void Awake()
    {
        //현재 로컬 플레이어의 진영이 어디냐에 따라서 현재 함정을 가시화 시킬지 비가시화 할 지에 대해서 결정함
        //아니면 여기에서 플레이어 리스트를 가지고 있는 다른 스크립트에 호출함

        PV = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        VisibleCheck();

    }

    private void Update()
    {
        //VisibleCheck();
    }


    public void VisibleCheck()
    {
        if (GameManager.Instance.CheckLocalPlayerSide() == Camp.Bad)
        {
            //가시화
            spriteRenderer.color = Color.white;

        }
        else
        {
            //비가시화
            spriteRenderer.color = Color.clear;
        }

    }

    [PunRPC]
    void VisibleALLPlayer()
    {
        spriteRenderer.color = Color.white;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //해당 플레이어에게 특정 상태를 검
            //움직일 수 없으며 5~10초마다 데미지가 10씩 받게됨

            //ismoveable 꺼주기

            PV.RPC(nameof(VisibleALLPlayer), RpcTarget.All);


            Debug.Log("함정 발동");




        }

    }
}
