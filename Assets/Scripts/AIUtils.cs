using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;
using System;


public class MovementNode : Node, ICallNodeDataHandler<MovementNode>
{
    protected NodeState nodeState = NodeState.FAILURE;
    protected Actor actor;
    protected MovementStats movementStats;

    readonly Vector3 dir;
    public MovementNode(Actor _actor)
    {
        actor = _actor;
        movementStats = _actor.GetStatComponent<MovementStats>();
    }
    public MovementNode(Vector3 _dir)
    {
        dir = _dir;
    }
    public override NodeState Evaluate()
    {
        Vector3 cundir = GetData<Vector3>();
        if (cundir == new Vector3(0, 0, 0))
        {
            nodeState = NodeState.FAILURE;
            return nodeState;
        }
        else
        {
            if (nodeState != NodeState.RUNNING)
            {
                nodeState = NodeState.SUCCESS;
                return nodeState;
            }
        }

        return NodeState.SUCCESS;
    }

    public void SetData(Node data)
    {
        data.SetData(dir);
    }
}
public class InvisibilityNode : Node
{
    float skillTime;
    PhotonView photonView;

    PunRPC punRPC;
    public InvisibilityNode(int _time, PhotonView _photonView, PunRPC _punRPC)
    {
        skillTime = _time;
        photonView = _photonView;
        punRPC = _punRPC;
    }

    public override NodeState Evaluate()
    {
        if (skillTime <= 0)
        {

        }
        else
        {
            skillTime -= Time.deltaTime;
        }
        photonView.RPC(nameof(punRPC), RpcTarget.All);
        return NodeState.RUNNING;
    }
}