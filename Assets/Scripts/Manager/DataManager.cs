using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<string> Actor_List = new List<string>();
    public bool[] Actor_Choosed;
    //������ �Ŵ��� ���� ������ �����ϰ� ������ �ʿ��� �� ����

    public int[] ID_arr;


    public void Start()
    {
        Actor_Choosed = new bool[Actor_List.Count];
    }

    public void Update()
    {
        //��ŸƮ���� �ٷ� ������ �������� �̰� �� ���� ����Ǽ� ����� ���� ���޾ƿ�
        if(Input.GetKeyDown(KeyCode.C))
        {
            Set_Actor();
        }
    }


    public void Set_Actor() // bool �迭�� ���°� ���� true �� ��� ������ ����
    {
        /*
         ���� �÷��̾� �ο� ���� üũ�ؼ� ������ �÷��̾�鿡�� ���� ������ �����
         �� ���� ����� ��� �ϴ� ������ ������ ������ �ʵ��� �ؾ� ��
         �׷��� ���� ����� �Ƹ� ID�� ���ؼ� �̷����� �� �� ����
         ���� �÷��̾� ���� ���� üũ�ϰ� �ش� üũ�� �÷��̾� ��ũ��Ʈ���� ���� �並 ������ 
         ������ ���� �信�� ID�� ���ͼ� �����Ű�� �ϸ� �� �� ����
         */

        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        ID_arr = new int[Player.Length];
        //Actor_Choosed = new bool[Actor_List.Count];
        for(int i=0; i<Player.Length; i++)
        {
            ID_arr[i] = Player[i].GetComponent<PhotonView>().ViewID;
        }

        for(int i=0; i<ID_arr.Length; i++)
        {
            //Debug.Log(Player.Length);
            //Debug.Log(ID_arr[i]);
            int rand = Random.Range(0, Actor_List.Count);
            Debug.Log(rand);
            while (Actor_Choosed[rand] == true)//������ ƨ�ܿ�
            {
                Debug.Log("��ħ");
                rand = Random.Range(0, Actor_List.Count);
            }
            Player[i].GetComponent<PlayerController>().Actor_What = Actor_List[rand];
            Actor_Choosed[rand]=true;

        }






    }




}
