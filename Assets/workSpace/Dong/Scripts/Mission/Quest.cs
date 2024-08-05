using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    protected int QuestID;

    protected bool AlreadySet; //이미 세팅이 되어있는지 이전에 해당 미션이 실행됬었는지에 대한 것을 체크해줌


    //이거 상속받도록 만들어서 관리하게 편하도록 해주기    
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
