using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class Chaser : Actor
{
    public GameObject DropObj;
    public override void Awake()
    {
        base.Awake();
        statComponents.Add(new MovementStats());
        AddNode<MovementNode>(new MovementNode(this), true);
        AddNode<MovementNode>(new ChaserDrop(DropObj), true);
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


public class ChaserDrop : Node
{
    float cunTime;
    readonly GameObject DropObj;
    public ChaserDrop(GameObject _dropObj)
    {
        DropObj = _dropObj;
    }
    public override NodeState Evaluate()
    {
        cunTime++;
        if (cunTime > 1000)
        {
            int rand = Random.Range(1, 10);
            if (rand > 7)
            {
                Debug.Log("에헤이");
            }
            cunTime = 0;
        }

        return NodeState.SUCCESS;
    }
}