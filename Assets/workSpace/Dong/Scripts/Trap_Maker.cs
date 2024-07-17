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

        //어떤 기능을 추가해야 할지 미리 정해야함


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
