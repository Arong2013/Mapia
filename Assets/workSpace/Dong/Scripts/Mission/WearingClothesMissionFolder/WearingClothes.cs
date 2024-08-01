using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearingClothes : MonoBehaviour,IQuestInitalize
{
    public List<Sprite> ClothesList = new List<Sprite>();
    
    public GameObject Clothes;
    public List<GameObject> ClothesObjList = new List<GameObject>();

    public Vector2[] AreaToUse_left = new Vector2[2];
    public Vector2[] AreaToUse_right = new Vector2[2];



    public DollClothesList WearingClothesList; //뭐 입을 건지 옷을 보여줌


    // Start is called before the first frame update
    void Start()
    {
        WearingClothesList = GetComponentInChildren<DollClothesList>();
        InitializeQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void InitializeQuest() //퀘스트 초기화
    {
        WearingClothesList.SetImgSprite(ClothesList); //입어야 하는 옷에 대한 리스트를 띄워줍니다.
        MakeClothes();



    }

    public void MakeClothes()
    {
        if(ClothesObjList.Count == 0)
        {
            int num = 0;
            foreach (var clothesImg in ClothesList)
            {
                GameObject ClothesObj = Instantiate(Clothes, transform);
                ClothesObj.GetComponent<Image>().sprite = clothesImg; 
                RectTransform rect = ClothesObj.GetComponent<RectTransform>();
                rect.anchoredPosition = SetPos();
        }
        }
        
    }

    public Vector2 SetPos()
    {
        int randomNum = Random.Range(0, 2);

        if(randomNum == 0)
        {
            float RandomX = Random.Range(AreaToUse_left[0].x, AreaToUse_left[1].x);
            float RandomY = Random.Range(AreaToUse_left[0].y, AreaToUse_left[1].y);
            return new Vector2(RandomX, RandomY);
        }
        else
        {
            float RandomX = Random.Range(AreaToUse_right[0].x, AreaToUse_right[1].x);
            float RandomY = Random.Range(AreaToUse_right[0].y, AreaToUse_right[1].y);
            return new Vector2(RandomX, RandomY);
        }
    }

    //void SetDustPos()
    //{
    //    Vector2[] Vector_Dust_Pos = new Vector2[3];

    //    for (int i = 0; i < 3; i++)
    //    {
    //        Vector_Dust_Pos[i] = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100));
    //    }



    //    while (Vector2.Distance(Vector_Dust_Pos[0], Vector_Dust_Pos[1]) < 130)
    //    {
    //        //Debug.Log("i : " + i + " , " + " j : " + j);
    //        // Debug.Log("i : "+ i + " " + Vector2.Distance(Vector_Dust_Pos[i], Vector_Dust_Pos[j]));
    //        Vector_Dust_Pos[1] = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100));
    //    }

    //    Vector_Dust_Pos[2] = -Vector_Dust_Pos[1];

    //    for (int i = 0; i < 3; i++)
    //    {
    //        Dust_Pos.Add(Vector_Dust_Pos[i]);
    //    }

    //}





}
