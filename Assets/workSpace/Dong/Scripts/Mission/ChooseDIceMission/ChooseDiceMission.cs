using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDiceMission : MonoBehaviour
{
    public GameObject Cup;
    public GameObject CupInDice;


    public float StartTime = 0f;


    int Check = 0;
    bool AlreadyMake = false;
    int count = 0;


    public List<RectTransform> CupRectLIst  = new List<RectTransform>();
    public List<Vector2>CupPos = new List<Vector2>();



    int Num = 3;
    int mixcount = 5;


    public int StartNum;
    public List<int> StartNumList= new List<int>();
    
    List<int> StartNum_Initalize = new List<int> { 0, 1, 2 };




    public int EndNum;
    public List<int> EndNumList = new List<int>();
    List<int> EndNumList_Initalize = new List<int> { 0, 1, 2 };




    private void Start()
    {

        for (int i = -1; i < 2; i++)
        {
            int j = 400;
            CupPos.Add(new Vector2(j * i, 0));
        }

        //StartNumList= StartNum_Initalize;
        //EndNumList = EndNumList_Initalize;
        MakeGame();
    }

    void RandomizeCup()
    {
        int AddCount = 0;
        while(AddCount < 5)
        {
            int RandomNum = Random.Range(0, 3);
            StartNum = RandomNum;
            StartNumList.Add(RandomNum);


            RandomNum = Random.Range(0, 3);
            EndNum = RandomNum;
            while (StartNum == EndNum)
            {
                RandomNum = Random.Range(0, 3);
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

            Debug.Log(CupRectLIst[StartNumList[count]].gameObject.name);
            Debug.Log(CupRectLIst[EndNumList[count]].gameObject.name);
            while (StartTime <= 4f)
            {
                //Debug.Log(StartTime);
                StartTime += Time.deltaTime;

                //CupPos[StartNum] = CupRectLIst[StartNum].anchoredPosition;
                //CupPos[EndNum] = CupRectLIst[EndNum].anchoredPosition;


               
                CupRectLIst[StartNumList[count]].anchoredPosition = Vector3.Lerp(CupRectLIst[StartNumList[count]].anchoredPosition, CupPos[EndNumList[count]], StartTime / 4f);
                
                
                CupRectLIst[EndNumList[count]].anchoredPosition = Vector3.Lerp(CupRectLIst[EndNumList[count]].anchoredPosition, CupPos[StartNumList[count]], StartTime / 4f);
                
                yield return null;
            }
            //StartTime = 0f;
            yield return new WaitForSeconds(1);
            StartCoroutine(MoveCup(count-1));
        }


       


    }



    void MakeGame()
    {




        if(CupRectLIst .Count == 0)
        {
            while (count < 3)
            {
                int randomNum = Random.Range(0, 2);

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
        CupRectLIst.Add(cupTransform);

        //CupPos.Add(cupTransform);
    }

    void InstantiateCupinDice()
    {
        RectTransform cupTransform = Instantiate(CupInDice, transform).GetComponent<RectTransform>();
        cupTransform.anchoredPosition = CupPos[count];
        AlreadyMake = true;
        count++;
        CupRectLIst.Add(cupTransform);
        //CupPos.Add(cupTransform);
    }









}


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