using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    protected int QuestID;

    protected bool AlreadySet; //�̹� ������ �Ǿ��ִ��� ������ �ش� �̼��� ������������� ���� ���� üũ����


    //�̰� ��ӹ޵��� ���� �����ϰ� ���ϵ��� ���ֱ�    
    //public Quest quest;
    protected virtual void Awake()
    {
        QuestID = 0;
    }
    //public abstract void GetMission(Quest quest);
    public abstract void InitalizeQuest();


    public abstract void ClearQuest();

    public abstract int GetQuestID();
}
