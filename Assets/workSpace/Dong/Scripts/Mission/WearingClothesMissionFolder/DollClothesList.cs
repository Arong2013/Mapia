using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollClothesList : MonoBehaviour
{
    public List<Image> ClothesImageList;

    public void Awake()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();

        foreach(var obj in images)
        {
            if(obj.gameObject != gameObject)
            {
                ClothesImageList.Add(obj);
            }
            
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImgSprite(List<Sprite> ClothesSpriteList)
    {
        //하위 오브젝트의 이미지 스프라이트에 이미지를 교체해줌
        List<int> clothesCountList = SetClothesCountList(ClothesSpriteList.Count);


        //랜덤으로 숫자를 뽑아오고 그 숫자에 해당하는 인덱스는 삭제시킴
        //그러면 따로 중복체크할 필요 없이 쉽게 중복없이 랜덤 값 얻을 수 있음
        foreach (var clothes in ClothesImageList)
        {
            int randNum = GetRandomNum(clothesCountList);
            clothes.sprite = ClothesSpriteList[clothesCountList[randNum]];
            clothesCountList.RemoveAt(randNum);
        }
    
    } //입어야하는 옷에 대한 이미지 리스트를 띄워줍니다.

    public List<int> SetClothesCountList(int ClothesCount)
    {
        int count = 0;
        List<int> clothesCountList = new List<int>();
        while (ClothesCount > count)
        {
            clothesCountList.Add(count);
            count++;
        }

        return clothesCountList;
    } //현재 받아온 리스트의 최대 인덱스 만큼 0부터 차례대로 하나씩 뽑아옴

    public int GetRandomNum(List<int> NumList)
    {
        int num = Random.Range(0, NumList.Count);
        return num;
    } //리스트 내의 값에서 랜덤으로 값을 뽑아줌




}
