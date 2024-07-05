using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
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
            //플레이어가 다가갔을 시 자동으로 퀘스트 자동으로 실행 
            //퀘스트는 일단 리스트로 지금은 단일 퀘스트 아이템 찾기 퀘스트로 작동되도록 함
            //근데 이게 랜덤으로 리스트 중에 퀘스트를 실행하도록 하는게 아니라
            //특정 장소에서 지정된 퀘스트만 실행되도록 하는 게 맞는 것 같은데..

            //일단 찾는 퀘스트 작동되도록
            
        }
    }
}
