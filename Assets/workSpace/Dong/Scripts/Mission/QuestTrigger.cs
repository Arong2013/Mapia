using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //�÷��̾ �ٰ����� �� �ڵ����� ����Ʈ �ڵ����� ���� 
            //����Ʈ�� �ϴ� ����Ʈ�� ������ ���� ����Ʈ ������ ã�� ����Ʈ�� �۵��ǵ��� ��
            //�ٵ� �̰� �������� ����Ʈ �߿� ����Ʈ�� �����ϵ��� �ϴ°� �ƴ϶�
            //Ư�� ��ҿ��� ������ ����Ʈ�� ����ǵ��� �ϴ� �� �´� �� ������..

            //�ϴ� ã�� ����Ʈ �۵��ǵ���
            
        }
    }
}
