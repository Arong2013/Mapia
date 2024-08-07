using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    protected int QuestID;

    //�̰� ��ӹ޵��� ���� �����ϰ� ���ϵ��� ���ֱ�    
    //public Quest quest;
    protected virtual void Awake()
    {
        QuestID = 0;
    }
    //public abstract void GetMission(Quest quest);
    public abstract void InitalizeQuest();
    public abstract int GetQuestID();
}



