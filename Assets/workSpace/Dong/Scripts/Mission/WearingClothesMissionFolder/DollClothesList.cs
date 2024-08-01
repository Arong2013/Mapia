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
        //���� ������Ʈ�� �̹��� ��������Ʈ�� �̹����� ��ü����
        List<int> clothesCountList = SetClothesCountList(ClothesSpriteList.Count);


        //�������� ���ڸ� �̾ƿ��� �� ���ڿ� �ش��ϴ� �ε����� ������Ŵ
        //�׷��� ���� �ߺ�üũ�� �ʿ� ���� ���� �ߺ����� ���� �� ���� �� ����
        foreach (var clothes in ClothesImageList)
        {
            int randNum = GetRandomNum(clothesCountList);
            clothes.sprite = ClothesSpriteList[clothesCountList[randNum]];
            clothesCountList.RemoveAt(randNum);
        }
    
    } //�Ծ���ϴ� �ʿ� ���� �̹��� ����Ʈ�� ����ݴϴ�.

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
    } //���� �޾ƿ� ����Ʈ�� �ִ� �ε��� ��ŭ 0���� ���ʴ�� �ϳ��� �̾ƿ�

    public int GetRandomNum(List<int> NumList)
    {
        int num = Random.Range(0, NumList.Count);
        return num;
    } //����Ʈ ���� ������ �������� ���� �̾���




}
