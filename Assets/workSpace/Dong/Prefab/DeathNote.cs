using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNote : Actor
{
    DeathNote_Writing MyNote;

    bool isMoveable = true;


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
        if (isMoveable)
        {
            CallAct<MovementNode>(new MovementNode(movement));
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
