using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestTrigger : MonoBehaviour, IPointerClickHandler
{
    public Quest missionName;


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
            Debug.Log("����");
            QuestManager.Instance.CheckQuest(missionName);

            //�÷��̾ �ٰ����� �� �ڵ����� ����Ʈ �ڵ����� ���� 
            //����Ʈ�� �ϴ� ����Ʈ�� ������ ���� ����Ʈ ������ ã�� ����Ʈ�� �۵��ǵ��� ��
            //�ٵ� �̰� �������� ����Ʈ �߿� ����Ʈ�� �����ϵ��� �ϴ°� �ƴ϶�
            //Ư�� ��ҿ��� ������ ����Ʈ�� ����ǵ��� �ϴ� �� �´� �� ������..

            //�ϴ� ã�� ����Ʈ �۵��ǵ���

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Ŭ������ �� ���� ����Ǿ��ִ� �̼� �г� �����
        Debug.Log("Ŭ��");
        //QuestManager.Instance.CheckQuest(missionName);
    }
}
