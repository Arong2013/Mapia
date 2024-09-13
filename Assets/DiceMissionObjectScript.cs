using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceMissionObjectScript : MonoBehaviour, IPointerClickHandler
{
    public Action IsDiceinCup;


    public Sprite Normal_State_Cup;
    public Sprite Open_State_Cup;

    Image CupImg;


    public bool Dice = false;

    public bool ChooseDice = false;

    public int NowPos;

    RectTransform rect;

    //만약 클릭했을 때 해당 클릭한 오브젝트의 위치 또는 애니메이션을 실행시켜


    //// Start is called before the first frame update
    void Start()
    {
        CupImg = GetComponent<Image>();

        rect = GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, 100);

        if (transform.parent.CompareTag("Dice"))
        {
            Dice = true;
        }

        StartCoroutine(CloseCup());
    }


    IEnumerator CloseCup()
    {

        //컵을 닫는 연출 정확히는 0으로 이동시키는 연출
         
        float TTime = 0f;

        while(TTime < 3f)
        {
            TTime += Time.deltaTime;
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, new Vector2(rect.anchoredPosition.x,0), TTime/3);
            yield return null;
        }
        




        
    }



    public void SelectReady()
    {
        ChooseDice = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if(ChooseDice)
        {
            ActivateAnimation();
            Debug.Log("컵 들어올리는 애니메이션 작동");
            if (Dice == true)
            {
                //gameClear; 게임 종료 
                IsDiceinCup?.Invoke();
                //이벤트 제작
            }
            else
            {

                Debug.Log("게임오버");
                IsDiceinCup?.Invoke();
                //gameInitialize; 게임 초기화
            }
        }
       

    }

    void ActivateAnimation()
    {
        //컵을 들어올렸을 때 주사위가 존재하냐 안하냐에 따라서 재시작 혹은 미션 클리어로 만들어줘야 할 것 같음

        StartCoroutine(OpenCup());

    }

    void SetNowPos(int num)
    {
        NowPos = num;
    }

    IEnumerator OpenCup()
    {
        //yield return new WaitForSeconds(1);
        
        //컵 이미지 변경 
        //컵의 위치를 변경
        float TTime = 0f;

        CupImg.sprite = Open_State_Cup;


        while (TTime < 2f)
        {
            TTime += Time.deltaTime;
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, new Vector2(rect.anchoredPosition.x, 100), TTime / 2);
            yield return null;
        }





    }

    void Initalizing()
    {
        //스프라이트 원본 스프라이트로 변경
        //위치 조정

        ChooseDice = false;


    }

    public void DiceIn()
    {
        Debug.Log("있어요");
    }

    public void DiceNotIn()
    {
        Debug.Log("없어요");
    }


}
