using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //�ش� �÷��̾�� Ư�� ���¸� ��
            //������ �� ������ 5~10�ʸ��� �������� 10�� �ްԵ�

            Debug.Log("���� �ߵ�");




        }

    }
}
