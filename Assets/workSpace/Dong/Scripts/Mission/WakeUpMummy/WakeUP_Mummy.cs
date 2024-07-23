using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

class WakeUP_Mummy : MonoBehaviour, IQuestInitalize
{
    public List<Sprite> MummyImg = new List<Sprite>(); //미라 이미지에 대한 정보를 저장해줌

    public Image Mummy;

    public GameObject Particles; //클릭을 했을 때 보여줄 파티클 이미지(애니메이션)

    public ClickPlace Range_Click; //클릭을 해야 하는 범위에 대한 이미지를 나타내는 오브젝트
   
    public GameObject ClearUI; //클리어 했다는 UI를 보여주는 오브젝트	

    Vector3 LeftUp = new Vector3(-300,170);  Vector3 RightDown = new Vector3(300, -170);
    //해당 범위내에서 클릭해야 하는 공간을 띄워줌

    int click_Amount = 40; //30~40 클릭해야 하는 횟수
    int click_num = 0; //클릭 몇번 했는지 체크해줄 변수

    int num = 1;

    public bool Clear = false;
    //다른 스크립트에 클리어를 했다는 것을 알려줄 변수

    void Start()
    {
        //RectTransform ck = Range_Click.GetComponent<RectTransform>();
        //ck.anchoredPosition = LeftUp;

        Mummy.sprite = MummyImg[0];
        Click_Range_Place(); //처음 시작 범위 지정
    }

    void Update()
    {
        MiniGame();
    }


    void MiniGame()
    {
        
        click_num = Range_Click.GetClick_num();

        if(click_num <click_Amount) 
        {
            if(click_num % (10 * num) == 0 && click_num != 0)
            {
                Click_Range_Place();
                num++;
                Mummy.sprite = MummyImg[num - 1];
            }

        }
        else
        {
            MiniGameClear();
        }

        
        //if (click_num == click_Amount)
        //{


        //    MiniGameClear();


        //}


    }

    void Click_Range_Place()
    {
        //LeftUp과 RightDown 내에서
        //클릭할 범위를 지정해주는 기능을 함(랜덤 요소 있음)
        if(Range_Click.gameObject.activeSelf == false)
        {
            Range_Click.gameObject.SetActive(true);
            RandomPlace();
        }
        else
        {
            RandomPlace();
        }
    }

    void RandomPlace() //랜덤 장소에 배치를 해주는 역할
    {
        RectTransform ck = Range_Click.GetComponent<RectTransform>();
        float randomXpos = Random.Range(LeftUp.x, RightDown.x);
        float randomYpos = Random.Range(LeftUp.y, RightDown.y);

        Vector2 randPos = new Vector2(randomXpos, randomYpos);

        while (Vector2.Distance(ck.anchoredPosition, randPos) < 80) //이전 위치에서 거리 차이가 어느정도 나도록 조건을 걸어줌
        {
            randomXpos = Random.Range(LeftUp.x, RightDown.x);
            randomYpos = Random.Range(LeftUp.y, RightDown.y);

            randPos = new Vector2(randomXpos, randomYpos);
        }

        ck.anchoredPosition = randPos;
    }

    void MiniGameClear()
    {
        //클리어 했다는 UI 띄워줌

        ClearUI.SetActive(true); //이거 UIUtils에서 관리해주면 될 것 같음


        Clear = true;
        //여기에서 게임 매니저나 다른 매니저에 게임이 끝났다는 것을 알림
    }

    public void InitalizeQuest() //미라깨우기 퀘스트 초기화
    {
        click_num = 0; //클릭 몇번 했는지 체크해줄 변수

        num = 1;

        Clear = false;
 
        Mummy.sprite = MummyImg[0];
        
        Click_Range_Place(); //처음 시작 범위 지정

        Range_Click.IntializeClick_Num();

    }




}
