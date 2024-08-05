using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    //����Ʈ ��� ������ �ְ� ����ֳĿ� ���� �� ��ġ�� �´� ����Ʈ �г� ����
    public List<Quest> QuestList;

    public GameObject ClearPanel;


    public void Awake()
    {
        foreach(var mission in gameObject.GetComponentsInChildren<Quest>())
        {
            QuestList.Add(mission);
        }
        ClearPanel = UiUtils.GetUI<QuestClear>().gameObject;
    }
    public void CheckQuest(Quest quest)
    {
        foreach(Quest Mission in QuestList)
        {
            if(quest == Mission)
            {
                Mission.gameObject.SetActive(true);
                Mission.GetComponent<Quest>().InitalizeQuest();
            }
            
        }
    }
    public void QuitObject()
    {
        foreach (var childobj in gameObject.GetComponentsInChildren<Quest>())
        {
            childobj.gameObject.SetActive(false);
        }


    }


    public void InitalizeQuest()
    {
        //����Ʈ �Ҵ�Ǿ��ִ°� �������ְ� ��ġ�� Ư�� Ʈ���� Ȯ���ϰ� �װſ� �°� ���ֱ�

        UiUtils.GetUI<QuestClear>().gameObject.SetActive(false);
    }



}
