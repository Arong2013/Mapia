using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public QuestPanel MissionPanel;
    
    public int QuestClearValueNum = 0; //퀘스트 클리어 횟수

    protected override void Awake()
    {
        base.Awake();
        MissionPanel =  UiUtils.GetUI<QuestPanel>();
    }


    //위치 혹은 특정 트리거 조건 확인하고 퀘스트 켜주는 기능
    public void CheckQuest(Quest quest)
    {
        if(MissionPanel.QuestList == null)
        {
            Debug.Log("nulll");
        }
        else
        {
            //Debug.Log(quest);
            //Debug.Log(MissionPanel.QuestList[0].GetComponent<Quest>());
        }

        foreach (Quest Mission in MissionPanel.QuestList)
        {
            //Debug.Log(quest.GetQuestID());

            if (quest.GetType() == Mission.GetType())
            {
                Debug.Log(Mission.gameObject.name);
                MissionPanel.gameObject.SetActive(true);
                MissionPanel.QuitObject();
                
                Mission.gameObject.SetActive(true);
                MissionPanel.InitalizeQuest();
                Mission.GetComponent<Quest>().InitalizeQuest();
            }
            else
            {
                //Debug.Log(quest.GetType());
                //Debug.Log(Mission.GetType());
            }
        }
    }

    public void QuestClear()
    {
        GameObject QPanel = UiUtils.GetUI<QuestPanel>().gameObject;
    
        QPanel.SetActive(false);
        QuestClearValueNum++;

    }

    public int ClearValue()
    {
        return QuestClearValueNum;
    }

}
