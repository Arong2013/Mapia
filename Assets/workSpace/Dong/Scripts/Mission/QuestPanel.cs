using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour,IQuestInitalize
{
    //퀘스트 목록 가지고 있고 어디있냐에 따라서 그 위치에 맞는 퀘스트 패널 켜줌
    public List<GameObject> QuestList = new List<GameObject>();



    //위치 혹은 특정 트리거 조건 확인하고 퀘스트 켜주는 기능


    public void InitalizeQuest()
    {
        //퀘스트 할당되어있는거 해제해주고 위치나 특정 트리거 확인하고 그거에 맞게 켜주기

        UiUtils.GetUI<QuestClear>().gameObject.SetActive(false);
    }



}
