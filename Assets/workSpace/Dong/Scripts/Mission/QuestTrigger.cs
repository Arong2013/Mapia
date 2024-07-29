using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestTrigger : MonoBehaviour, IPointerClickHandler
{
    public Quest missionName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("닿음");
            QuestManager.Instance.CheckQuest(missionName);

            //플레이어가 다가갔을 시 자동으로 퀘스트 자동으로 실행 
            //퀘스트는 일단 리스트로 지금은 단일 퀘스트 아이템 찾기 퀘스트로 작동되도록 함
            //근데 이게 랜덤으로 리스트 중에 퀘스트를 실행하도록 하는게 아니라
            //특정 장소에서 지정된 퀘스트만 실행되도록 하는 게 맞는 것 같은데..

            //일단 찾는 퀘스트 작동되도록

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //클릭했을 때 현재 적용되어있는 미션 패널 띄워줌
        Debug.Log("클릭");
        //QuestManager.Instance.CheckQuest(missionName);
    }
}
