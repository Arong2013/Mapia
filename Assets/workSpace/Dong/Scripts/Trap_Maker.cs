using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Maker : Actor
{
    bool isMoveable = true;
    public GameObject trap;



    protected override void Start()
    {
        base.Start();

        //� ����� �߰��ؾ� ���� �̸� ���ؾ���


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }




    public override void Move()
    {
        if (isMoveable)
        {
            CallAct<MovementNode>(new MovementNode(movement));
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
