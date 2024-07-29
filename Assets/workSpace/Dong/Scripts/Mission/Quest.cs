using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    protected int QuestID;

    //이거 상속받도록 만들어서 관리하게 편하도록 해주기    
    //public Quest quest;


    protected virtual void Awake()
    {
        QuestID = 0;
    }

    //public abstract void GetMission(Quest quest);

    public abstract void InitalizeQuest();

    public abstract int GetQuestID();


}
