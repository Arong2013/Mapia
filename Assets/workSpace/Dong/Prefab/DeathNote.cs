using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNote : Actor
{
    DeathNote_Writing MyNote;

    //��ǲ �ʵ� �ʿ���
    //��ǲ �ʵ带 ���� ��ư �ʿ���
    //�ϴ� Ű �Է��� �޾Ƽ� �ش� ��ǲ �ʵ带 ���ִ� �ɷ� ������
    //�� UI�� �ϴ� �÷��̾� ���ο� ����� ���߿� ���������� �������� �� �� ����
    //��� UI���� Ư�� ������Ʈ���� ������ ���� ���� ���� �� ����

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
