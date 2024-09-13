using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDiceMission : Quest
{
    //97번줄 이벤트 수정해야함중요


    public GameObject Cup;
    public GameObject CupInDice;

    public Action ClearQuestevt;


    public float StartTime = 0f;


    int Check = 0;
    bool AlreadyMake = false;
    int count = 0;

    bool ChooseActivate = false;


    public List<RectTransform> CupRectList  = new List<RectTransform>();
    public List<DiceMissionObjectScript> CupObjList = new List<DiceMissionObjectScript>();
    public List<Vector2>CupPos = new List<Vector2>();




    public Vector2 startpos;
    public Vector2 endpos;


    public int StartNum;
    public List<int> StartNumList= new List<int>();

    public int EndNum;
    public List<int> EndNumList = new List<int>();


    protected override void Start()
    {

        for (int i = -1; i < 2; i++)
        {
            int j = 400;
            CupPos.Add(new Vector2(j * i, 0));
        }

        if (CupRectList.Count == 0)
        {
            while (count < 3)
            {
                int randomNum = UnityEngine.Random.Range(0, 2);

                if (randomNum == 0)
                {
                    if (Check < 2)
                    {
                        InstantiateJustCup();
                    }


                }
                else
                {

                    if (!AlreadyMake)
                    {
                        InstantiateCupinDice();
                    }

                }

            }
        }

        StartCoroutine(MoveCup_2(5));



       

       
    }

    private void Update()
    {
        if (ChooseActivate) //이벤트로 전환예정
        {
            Debug.Log(CupObjList.Count);

            foreach (var cupp in CupObjList) //현재 미션 초기 구성에서 가장 마지막에 이뤄져야함
            {
                cupp.SelectReady();
                cupp.IsDiceinCup += ClearQuestevt; //수정해야함 
            }
            ChooseActivate = false;
        }
    }



    void ChangeChange(out int randNum,out int randNum_2)
    {
        randNum = UnityEngine.Random.Range(0, 3);

        startpos = CupRectList[randNum].anchoredPosition;


        randNum_2 = UnityEngine.Random.Range(0, 3);

        while (randNum_2 == randNum)
        {
            randNum_2 = UnityEngine.Random.Range(0, 3);
        }
        endpos = CupRectList[randNum_2].anchoredPosition;


        //Debug.Log(startpos);
        //Debug.Log(endpos);

        //Debug.Log(randNum + " , " + randNum_2);
       // StartCoroutine(MoveCup_2(randNum, randNum_2));

    }



    void RandomizeCup()
    {
        int AddCount = 0;
        while(AddCount < 5)
        {
            int RandomNum = UnityEngine.Random.Range(0, 3);
            StartNum = RandomNum;
            StartNumList.Add(RandomNum);


            RandomNum = UnityEngine.Random.Range(0, 3);
            EndNum = RandomNum;
            while (StartNum == EndNum)
            {
                RandomNum = UnityEngine.Random.Range(0, 3);
                EndNum = RandomNum;
            }
            EndNumList.Add(RandomNum);
            Debug.Log(StartNum + " , " + EndNum);
            AddCount++;
        }
        StartCoroutine(MoveCup(AddCount-1));
        //if (mixcount <5)
        //{
        //    StartCoroutine(MoveCup());
        //}

    }

    IEnumerator MoveCup(int count)
    {
        if(count >1)
        {
            StartTime = 0f;

            Debug.Log(StartNumList[count] + "   ,   " + EndNumList[count]);

            Debug.Log(CupRectList[StartNumList[count]].gameObject.name);
            Debug.Log(CupRectList[EndNumList[count]].gameObject.name);
            while (StartTime <= 4f)
            {
                //Debug.Log(StartTime);
                StartTime += Time.deltaTime;

                //CupPos[StartNum] = CupRectList[StartNum].anchoredPosition;
                //CupPos[EndNum] = CupRectList[EndNum].anchoredPosition;


               
                CupRectList[StartNumList[count]].anchoredPosition = Vector3.Lerp(CupRectList[StartNumList[count]].anchoredPosition, CupPos[EndNumList[count]], StartTime / 4f);
                
                
                CupRectList[EndNumList[count]].anchoredPosition = Vector3.Lerp(CupRectList[EndNumList[count]].anchoredPosition, CupPos[StartNumList[count]], StartTime / 4f);
                
                yield return null;
            }
            //StartTime = 0f;
            yield return new WaitForSeconds(1);
            StartCoroutine(MoveCup(count-1));
        }


       


    }

    IEnumerator MoveCup_2(int trycount)
    {

        yield return new WaitForSeconds(0.5f);

        if(trycount >0)
        {
            int rand_start, rand_end;

            ChangeChange(out rand_start, out rand_end);
            Debug.Log(rand_start + " , " + rand_end);

            StartTime = 0f;
            while (StartTime <= 0.2f)
            {
                //Debug.Log(StartTime);
                StartTime += Time.deltaTime;

                //CupPos[StartNum] = CupRectList[StartNum].anchoredPosition;
                //CupPos[EndNum] = CupRectList[EndNum].anchoredPosition;



                CupRectList[rand_start].anchoredPosition = Vector3.Lerp(CupRectList[rand_start].anchoredPosition, endpos, StartTime / 1f);


                CupRectList[rand_end].anchoredPosition = Vector3.Lerp(CupRectList[rand_end].anchoredPosition, startpos, StartTime / 1f);

                yield return null;
            }
            //StartTime = 0f;
            //yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveCup_2(trycount - 1));
        }
        else
        {
            ChooseActivate = true;
            yield return null;
        }
        

    }

    void MakeGame()
    {




        if(CupRectList.Count == 0)
        {
            while (count < 3)
            {
                int randomNum = UnityEngine.Random.Range(0, 2);

                if (randomNum == 0)
                {
                    if (Check < 2)
                    {
                        InstantiateJustCup();
                    }


                }
                else
                {

                    if (!AlreadyMake)
                    {
                        InstantiateCupinDice();
                    }

                }

            }
        }



        //랜덤으로 컵을 섞어주는 애니메이션이 존재함 

        RandomizeCup();












    }



    void InstantiateJustCup()
    {
        RectTransform cupTransform = Instantiate(Cup, transform).GetComponent<RectTransform>();
        cupTransform.anchoredPosition = CupPos[count];
        Check++;
        count++;
        CupRectList.Add(cupTransform);
        CupObjList.Add(cupTransform.gameObject.GetComponent<DiceMissionObjectScript>());
        //CupPos.Add(cupTransform);
    }

    void InstantiateCupinDice()
    {
        RectTransform cupTransform = Instantiate(CupInDice, transform).GetComponent<RectTransform>();
        cupTransform.anchoredPosition = CupPos[count];
        AlreadyMake = true;
        count++;
        CupRectList.Add(cupTransform);
        CupObjList.Add(cupTransform.transform.GetComponentInChildren<DiceMissionObjectScript>());
        //CupPos.Add(cupTransform);
    }





    void ChangeCupPos()
    {
        //GameObject[] cups;

        //if (cups.Length == 0) return;

        //// 현재 컵들의 위치를 저장
        //Vector3[] initialPositions = new Vector3[cups.Length];
        //for (int i = 0; i < cups.Length; i++)
        //{
        //    initialPositions[i] = cups[i].transform.position;
        //}

        //// 컵들의 위치를 서로 교환 (여기서는 첫 번째 컵이 마지막 컵으로, 나머지 컵은 한 칸씩 앞으로 이동)
        //for (int i = 0; i < cups.Length; i++)
        //{
        //    int nextIndex = (i + 1) % cups.Length; // 마지막 컵이 첫 번째로 이동하게끔 설정
        //    cups[i].transform.position = initialPositions[nextIndex];
        //}


    }

    protected override void ClearQuest()
    {
        //CupObjList.
        base.ClearQuest();
        QuestManager.Instance.OpenClearPanel();
    }


    public override void InitalizeQuest()
    {
        throw new System.NotImplementedException();
    }

    public override int GetQuestID()
    {
        throw new System.NotImplementedException();
    }
}






//MoveCup_2();

//int randNum = Random.Range(0, 3);
//startpos = CupRectList[randNum].anchoredPosition;


//int randNum_2 = Random.Range(0, 3);

//while(randNum_2 == randNum)
//{
//    randNum_2 = Random.Range(0, 3);
//}
//endpos = CupRectList[randNum_2].anchoredPosition;


//Debug.Log(startpos);
//Debug.Log(endpos);

//Debug.Log(randNum + " , " + randNum_2);
//StartCoroutine(MoveCup_2(randNum, randNum_2));



//StartNumList= StartNum_Initalize;
//EndNumList = EndNumList_Initalize;

///주석 해제할 거
//MakeGame();








/*
 public class ChangePos : MonoBehaviour
{

    public GameObject[] objs;
    public Vector2[] pos;

    public bool Check = false;

    // Start is called before the first frame update
    void Start()
    {
        pos[0] = objs[0].transform.position;

        Debug.Log(pos[0]);

        pos[1] = objs[1].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePosMethod();
        }

        if(Check)
        {
            ChangePosMethod();
        }

    }

    void ChangePosMethod()
    {
        StartCoroutine(ChangePosCorutine());

    }

    IEnumerator ChangePosCorutine()
    {
        float StartTime = 0f;


        while(StartTime < 4f)
        {
            StartTime += Time.deltaTime;
            objs[0].transform.position = Vector3.Lerp(objs[0].transform.position, pos[1], StartTime/ 4f);
            objs[1].transform.position = Vector3.Lerp(objs[1].transform.position, pos[0], StartTime / 4f);
            yield return null;
        }
    }
}
 
 
 */