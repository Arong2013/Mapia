using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

class WakeUP_Mummy : MonoBehaviour, IQuestInitalize
{
    public List<Sprite> MummyImg = new List<Sprite>(); //�̶� �̹����� ���� ������ ��������

    public Image Mummy;

    public GameObject Particles; //Ŭ���� ���� �� ������ ��ƼŬ �̹���(�ִϸ��̼�)

    public ClickPlace Range_Click; //Ŭ���� �ؾ� �ϴ� ������ ���� �̹����� ��Ÿ���� ������Ʈ
   
    public GameObject ClearUI; //Ŭ���� �ߴٴ� UI�� �����ִ� ������Ʈ	

    Vector3 LeftUp = new Vector3(-300,170);  Vector3 RightDown = new Vector3(300, -170);
    //�ش� ���������� Ŭ���ؾ� �ϴ� ������ �����

    int click_Amount = 40; //30~40 Ŭ���ؾ� �ϴ� Ƚ��
    int click_num = 0; //Ŭ�� ��� �ߴ��� üũ���� ����

    int num = 1;

    public bool Clear = false;
    //�ٸ� ��ũ��Ʈ�� Ŭ��� �ߴٴ� ���� �˷��� ����

    void Start()
    {
        //RectTransform ck = Range_Click.GetComponent<RectTransform>();
        //ck.anchoredPosition = LeftUp;

        Mummy.sprite = MummyImg[0];
        Click_Range_Place(); //ó�� ���� ���� ����
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
        //LeftUp�� RightDown ������
        //Ŭ���� ������ �������ִ� ����� ��(���� ��� ����)
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

    void RandomPlace() //���� ��ҿ� ��ġ�� ���ִ� ����
    {
        RectTransform ck = Range_Click.GetComponent<RectTransform>();
        float randomXpos = Random.Range(LeftUp.x, RightDown.x);
        float randomYpos = Random.Range(LeftUp.y, RightDown.y);

        Vector2 randPos = new Vector2(randomXpos, randomYpos);

        while (Vector2.Distance(ck.anchoredPosition, randPos) < 80) //���� ��ġ���� �Ÿ� ���̰� ������� ������ ������ �ɾ���
        {
            randomXpos = Random.Range(LeftUp.x, RightDown.x);
            randomYpos = Random.Range(LeftUp.y, RightDown.y);

            randPos = new Vector2(randomXpos, randomYpos);
        }

        ck.anchoredPosition = randPos;
    }

    void MiniGameClear()
    {
        //Ŭ���� �ߴٴ� UI �����

        ClearUI.SetActive(true); //�̰� UIUtils���� �������ָ� �� �� ����


        Clear = true;
        //���⿡�� ���� �Ŵ����� �ٸ� �Ŵ����� ������ �����ٴ� ���� �˸�
    }

    public void InitalizeQuest() //�̶����� ����Ʈ �ʱ�ȭ
    {
        click_num = 0; //Ŭ�� ��� �ߴ��� üũ���� ����

        num = 1;

        Clear = false;
 
        Mummy.sprite = MummyImg[0];
        
        Click_Range_Place(); //ó�� ���� ���� ����

        Range_Click.IntializeClick_Num();

    }




}
