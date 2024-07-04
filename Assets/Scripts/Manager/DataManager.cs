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

    public int[] ID_arr;


    public void Start()
    {
        Actor_Choosed = new bool[Actor_List.Count];
    }

    public void Update()
    {
        //스타트에서 바로 됬으면 좋겠지만 이게 더 빨리 실행되서 제대로 값을 못받아옴
        if(Input.GetKeyDown(KeyCode.C))
        {
            Set_Actor();
        }
    }


    public void Set_Actor() // bool 배열의 상태가 전부 true 일 경우 게임이 멈춤
    {
        /*
         현재 플레이어 인원 수를 체크해서 각각의 플레이어들에게 랜덤 직업을 배분함
         단 직업 배분의 경우 일단 동일한 직업을 가지지 않도록 해야 함
         그래서 직업 배분은 아마 ID를 통해서 이뤄지게 될 것 같음
         현재 플레이어 수를 전부 체크하고 해당 체크한 플레이어 스크립트에서 포톤 뷰를 가져옴 
         가져온 포톤 뷰에서 ID를 빼와서 적용시키게 하면 될 것 같음
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
            while (Actor_Choosed[rand] == true)//게임이 튕겨요
            {
                Debug.Log("겹침");
                rand = Random.Range(0, Actor_List.Count);
            }
            Player[i].GetComponent<PlayerController>().Actor_What = Actor_List[rand];
            Actor_Choosed[rand]=true;

        }






    }




}
