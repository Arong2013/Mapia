using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    protected int QuestID;

    protected bool AlreadySet;

    Actor actor;



    //public Quest quest;
    protected virtual void Awake()
    {
        QuestID = 0;
    }

    protected virtual void Start()
    {
        Questinprogress();
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
        actor.FinishMission();
    }

    public abstract int GetQuestID();

    public void GetActor(Actor actor)
    {
        Debug.Log(gameObject.name);
        this.actor = actor; 
    }


}
