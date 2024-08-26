using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    protected int QuestID;

    protected bool AlreadySet;

    public static Actor actor;



    //public Quest quest;
    protected virtual void Awake()
    {
        QuestID = 0;
    }

    protected virtual void Start()
    {
        
    }


    //public abstract void GetMission(Quest quest);
    public abstract void InitalizeQuest();


    protected virtual void Questinprogress()
    {
        //현재 퀘스트를 진행중인 액터가 움직이지 못하도록 해야 함
        actor.DoingMission();
    }


    protected virtual void ClearQuest()
    {
        if(actor != null)
        {
            actor.FinishMission();
        }
        else
        {
            Debug.Log("null");
        }
        
    }

    public abstract int GetQuestID();

    public void GetActor(Actor _actor)
    {
        if(actor == null)
        {
            //Debug.Log(_actor.NickNameText.text);
            actor = _actor;
            Questinprogress();
        }
        else
        {
            Questinprogress();
        }

        
    }


}



