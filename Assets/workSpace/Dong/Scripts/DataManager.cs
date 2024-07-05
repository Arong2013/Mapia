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
    PhotonView pv;
    public int[] ID_arr;
    int[] rand;


    public void Start()
    {
        pv = GetComponent<PhotonView>();
        Actor_Choosed = new bool[Actor_List.Count];
    }

    public void Update()
    {
        //��ŸƮ���� �ٷ� ������ �������� �̰� �� ���� ����Ǽ� ����� ���� ���޾ƿ�
        if (Input.GetKeyDown(KeyCode.C))
        {
            Set_Actor_Num();
            pv.RPC("Set_Actor", RpcTarget.All, rand, ID_arr);
            //Set_Actor();
        }
    }


    [PunRPC]
    public void Set_Actor_Num() // bool �迭�� ���°� ���� true �� ��� ������ ����
    {
        /*
         ���� �÷��̾� �ο� ���� üũ�ؼ� ������ �÷��̾�鿡�� ���� ������ �����
         �� ���� ����� ��� �ϴ� ������ ������ ������ �ʵ��� �ؾ� ��
         �׷��� ���� ����� �Ƹ� ID�� ���ؼ� �̷����� �� �� ����
         ���� �÷��̾� ���� ���� üũ�ϰ� �ش� üũ�� �÷��̾� ��ũ��Ʈ���� ���� �並 ������ 
         ������ ���� �信�� ID�� ���ͼ� �����Ű�� �ϸ� �� �� ����
         */

        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");
        rand = new int[Player.Length];
        ID_arr = new int[Player.Length];
        //Actor_Choosed = new bool[Actor_List.Count];
        for (int i = 0; i < Player.Length; i++)
        {
            ID_arr[i] = Player[i].GetComponent<PhotonView>().ViewID;
        }

        for (int i = 0; i < ID_arr.Length; i++)
        {
            //Debug.Log(Player.Length);
            //Debug.Log(ID_arr[i]);
            rand[i] = Random.Range(0, Actor_List.Count);
            while (Actor_Choosed[rand[i]] == true)//������ ƨ�ܿ�
            {
                Debug.Log("��ħ");
                rand[i] = Random.Range(0, Actor_List.Count);
            }
            Actor_Choosed[rand[i]] = true;

        }
    }

    [PunRPC]
    public void Set_Actor(int[] num_arr, int[] id)
    {
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        if(ID_arr.Length == 0)
        {
            ID_arr = new int[Player.Length];
            //Actor_Choosed = new bool[Actor_List.Count];
            ID_arr = id;
        }
        

        for (int i = 0; i < Player.Length; i++)
        {


            for (int j = 0; j < Player.Length; j++)
            {
                Debug.Log(ID_arr[i]);
                if (ID_arr[i] == Player[j].GetComponent<PhotonView>().ViewID)
                {
                    Player[j].GetComponent<Dectective>().Actor_What = Actor_List[num_arr[i]];
                }

            }
        }

    }


}
