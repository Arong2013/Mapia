using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour,IQuestInitalize
{
    //����Ʈ ��� ������ �ְ� ����ֳĿ� ���� �� ��ġ�� �´� ����Ʈ �г� ����
    public List<GameObject> QuestList = new List<GameObject>();



    //��ġ Ȥ�� Ư�� Ʈ���� ���� Ȯ���ϰ� ����Ʈ ���ִ� ���


    public void InitalizeQuest()
    {
        //����Ʈ �Ҵ�Ǿ��ִ°� �������ְ� ��ġ�� Ư�� Ʈ���� Ȯ���ϰ� �װſ� �°� ���ֱ�

        UiUtils.GetUI<QuestClear>().gameObject.SetActive(false);
    }



}
