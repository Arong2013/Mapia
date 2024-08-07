using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Quest 1

public class CleanDustQuest : Quest
{
    
    public static int Count = 3;
    public List<Vector2> Dust_Pos;

    public Dust[] dusts = new Dust[Count];

    public Wipe wipe;


    public GameObject Jewel;
    public GameObject DustPrefab; //이미 3개가 생성되어있다면 위치만 변경시켜주도록 함
    public GameObject TaskCompleteText;
    
    public bool Clean = false;

    protected override void Awake()
    {
        QuestID = 1;
        AlreadySet = false;
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        TaskCompleteText = UiUtils.GetUI<QuestClear>().gameObject;

        //먼지 위치 랜덤으로 설정해줘야함

        //SetDustPos();

        //for (int i = 0; i < Count;i++)
        //{
        //    GameObject Dust = Instantiate(DustPrefab,Jewel.transform);
        //    dusts[i] = Dust.GetComponent<Dust>();
        //    RectTransform dust_rect = Dust.GetComponent<RectTransform>();

        //    dust_rect.anchoredPosition = Dust_Pos[i];
            
        //    Debug.Log(dust_rect.rect);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestClearCheck())
        {
            //나중에 퀘스트 매니저가 들어가면 사용할 수 있음
            Clean = true;

            if(Clean == true)
            {
                ClearQuest();
            }
            
        }
        


    }

    void SetDustPos()
    {
        Vector2[] Vector_Dust_Pos = new Vector2[3];

        for(int i=0; i< 3; i++)
        {
            Vector_Dust_Pos[i] = new Vector2(UnityEngine.Random.Range(-100,100),UnityEngine.Random.Range(-100,100));
        }

      
     
         while (Vector2.Distance(Vector_Dust_Pos[0], Vector_Dust_Pos[1]) < 130)
         {
            //Debug.Log("i : " + i + " , " + " j : " + j);
                // Debug.Log("i : "+ i + " " + Vector2.Distance(Vector_Dust_Pos[i], Vector_Dust_Pos[j]));
            Vector_Dust_Pos[1] = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100));
         }

        Vector_Dust_Pos[2] = -Vector_Dust_Pos[1];

        if(Dust_Pos.Count >0)
        {
            Dust_Pos.Clear();
        }

         for (int i = 0; i < 3; i++)
        {
            Dust_Pos.Add(Vector_Dust_Pos[i]);
        }
        
    }

    void SetDust()
    {
        SetDustPos();

      
            for (int i = 0; i < Count; i++)
            {
                    RectTransform dust_rect;
            Debug.Log(dusts.Length);

                if (dusts[Count-1] == null)
                {
                    GameObject Dust = Instantiate(DustPrefab, Jewel.transform);
                    dusts[i] = Dust.GetComponent<Dust>();
                    dust_rect = Dust.GetComponent<RectTransform>();
                }
                else
                {
                    dust_rect = dusts[i].GetComponent<RectTransform>();
                }
                    dust_rect.anchoredPosition = Dust_Pos[i];

                    Debug.Log(dust_rect.rect);
            }
        
        
    }

    public bool QuestClearCheck()
    {
        for (int i = 0; i < Count; i++)
        {
            if (dusts[i].Erased == false)
            {
                return false;
            }
        }

        //조건을 다 통과했을 경우 true값 반환
        return true;


    }


    public override void InitalizeQuest()
    {
        Debug.Log("작동");

        SetDust();

        for (int i = 0; i < Count; i++)
        {
            dusts[i].Initalizing();
        }

        wipe.Initializing();
        Clean = false;
        //다른 것도 초기화시켜줘야함
        

    }

    public override int GetQuestID()
    {
        return QuestID;
    }

    protected override void ClearQuest()
    {
        base.ClearQuest();
        AlreadySet = true;
        QuestManager.Instance.OpenClearPanel();
    }


}
