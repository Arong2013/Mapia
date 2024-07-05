using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<string> Actor_List = new List<string>();
    public bool[] Actor_Choosed;
    //데이터 매니저 직업 정보를 저장하고 있으며 필요할 때 빼옴
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
        //스타트에서 바로 됬으면 좋겠지만 이게 더 빨리 실행되서 제대로 값을 못받아옴
        if (Input.GetKeyDown(KeyCode.C))
        {
            Set_Actor_Num();
            pv.RPC("Set_Actor", RpcTarget.All, rand, ID_arr);
            //Set_Actor();
        }
    }


    [PunRPC]
    public void Set_Actor_Num() // bool 배열의 상태가 전부 true 일 경우 게임이 멈춤
    {
        /*
         현재 플레이어 인원 수를 체크해서 각각의 플레이어들에게 랜덤 직업을 배분함
         단 직업 배분의 경우 일단 동일한 직업을 가지지 않도록 해야 함
         그래서 직업 배분은 아마 ID를 통해서 이뤄지게 될 것 같음
         현재 플레이어 수를 전부 체크하고 해당 체크한 플레이어 스크립트에서 포톤 뷰를 가져옴 
         가져온 포톤 뷰에서 ID를 빼와서 적용시키게 하면 될 것 같음
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
            while (Actor_Choosed[rand[i]] == true)//게임이 튕겨요
            {
                Debug.Log("겹침");
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
