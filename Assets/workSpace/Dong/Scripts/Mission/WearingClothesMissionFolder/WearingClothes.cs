using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearingClothes : Quest,IQuestInitalize
{
    static public WearingClothes instance;

    public List<int> ClothesToWear;
    public List<Sprite> ClothesList = new List<Sprite>();
    
    public GameObject Clothes;
    public List<Clothes> ClothesObjList = new List<Clothes>();
    public List<ClothesSlot>ClothesSlotList = new List<ClothesSlot>();

    public Vector2[] AreaToUse_left = new Vector2[2];
    public Vector2[] AreaToUse_right = new Vector2[2];

    public Clothes pick;


    public DollClothesList WearingClothesList; //뭐 입을 건지 옷을 보여줌




    protected override void Awake()
    {
        QuestID = 3;
        AlreadySet = false;
    }

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(ClearQuestCheck())
        {
            ClearQuest();
        }    
        
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
                ClothesObj.GetComponent<Clothes>().SetID(num);
                RectTransform rect = ClothesObj.GetComponent<RectTransform>();
                rect.anchoredPosition = SetPos();

                ClothesObjList.Add(ClothesObj.GetComponent<Clothes>());

                num++;
            }
        }
        else
        {
            foreach (var clothesObj in ClothesObjList)
            {
               
                RectTransform rect = clothesObj.GetComponent<RectTransform>();
                rect.anchoredPosition = SetPos();
                clothesObj.RaycastTargetOn();
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

    public bool ClearQuestCheck()
    {
        int count = 0;
        //여기에서 옷을 다 알맞게 입었는지 확인 후 체크해서 모두 다 올바르다면 true 하나라도 틀렸다면 false
        foreach (var obj in ClothesSlotList)
        {
            if(obj.CurrentID != obj.ID)
            {
                return false;
            }
        }
        return true;
    }

    public override void InitalizeQuest()
    {
        if(AlreadySet == false)
        {
            WearingClothesList = GetComponentInChildren<DollClothesList>();
            ClothesSlotList.AddRange(GetComponentsInChildren<ClothesSlot>());
        }
        else
        {
            Debug.Log("작동되고 있어요ㅕ");
        }




        WearingClothesList.SetImgSprite(ClothesList); //입어야 하는 옷에 대한 리스트를 띄워줍니다.
        ClothesToWear = WearingClothesList.GetrandomNumList();

        int count = 0;
        foreach (var slot in ClothesSlotList)
        {
            Debug.Log(slot.gameObject);
            slot.Initalizing(transform);
            slot.ID = ClothesToWear[count++];
        }
        MakeClothes();
    }

    public override int GetQuestID()
    {
        return QuestID;
    }

    public override void ClearQuest()
    {
        AlreadySet = true;
        QuestManager.Instance.OpenClearPanel();
    }
}
