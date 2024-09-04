using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceMissionObjectScript : MonoBehaviour, IPointerClickHandler
{


    public bool Dice = false;

    public bool ChooseDice = false;

    public int NowPos;



    //만약 클릭했을 때 해당 클릭한 오브젝트의 위치 또는 애니메이션을 실행시켜


    //// Start is called before the first frame update
    void Start()
    {
        if(transform.parent.CompareTag("Dice"))
        {
            Dice = true;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void OnPointerClick(PointerEventData eventData)
    {

        if(ChooseDice)
        {
            ActivateAnimation();
            Debug.Log("컵 들어올리는 애니메이션 작동");
            if (Dice == true)
            {
                //gameClear; 게임 종료 
            }
            else
            {
                //gameInitialize; 게임 초기화
            }
        }
       

    }

    void ActivateAnimation()
    {
        //컵을 들어올렸을 때 주사위가 존재하냐 안하냐에 따라서 재시작 혹은 미션 클리어로 만들어줘야 할 것 같음



    }

    void SetNowPos(int num)
    {
        NowPos = num;
    }




}
