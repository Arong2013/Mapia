using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNote : Actor
{
    DeathNote_Writing MyNote;

    //인풋 필드 필요함
    //인풋 필드를 켜줄 버튼 필요함
    //일단 키 입력을 받아서 해당 인풋 필드를 켜주는 걸로 정하자
    //이 UI는 일단 플레이어 내부에 만들고 나중에 프리팹으로 빼놓으면 될 것 같음
    //모든 UI들을 특정 오브젝트에만 넣으면 별로 좋지 않을 것 같음

    /*
     public void DestroyGameobject(string _name)
    {
    PV.RPC(nameof(RPCDestroyGameobject), RpcTarget.All, _name);
    }

    if(playersData.ContainsKey("ID"))
    {
    DestroyGameobject(playersData["ID"]);
    }
     
     */




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MyNote = UiUtils.GetUI<DeathNote_Writing>();
        Debug.Log(GetComponent<DeathNote>().ID);
        Debug.Log(PV.Owner.NickName);
    }

    // Update is called once per frame
    void Update()
    {
        //if(PV.IsMine)
        //{
        //    Move();
        //}
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(MyNote.gameObject.activeSelf == false)
            {
                MyNote.gameObject.SetActive(true);
            }
            else
            {
                MyNote.gameObject.SetActive(false);
            }
                  
        }
    }

    public override void Move()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
        RB.velocity = movement * 4f;
        CallAct<MovementNode>(new MovementNode(movement));
        PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
    }




}
