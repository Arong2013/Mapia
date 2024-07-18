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
        //���� ���� �÷��̾��� ������ ���Ŀ� ���� ���� ������ ����ȭ ��ų�� �񰡽�ȭ �� ���� ���ؼ� ������
        //�ƴϸ� ���⿡�� �÷��̾� ����Ʈ�� ������ �ִ� �ٸ� ��ũ��Ʈ�� ȣ����

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
            //����ȭ
            spriteRenderer.color = Color.white;

        }
        else
        {
            //�񰡽�ȭ
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
            //�ش� �÷��̾�� Ư�� ���¸� ��
            //������ �� ������ 5~10�ʸ��� �������� 10�� �ްԵ�

            //ismoveable ���ֱ�

            PV.RPC(nameof(VisibleALLPlayer), RpcTarget.All);


            Debug.Log("���� �ߵ�");




        }

    }
}
