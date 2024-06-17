using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;
using System;
public class MoveNode : Node
{
    Rigidbody2D rigidbody2D;
    Animator animator;

    PhotonView photonView;

    Ai ai;
    public MoveNode(Rigidbody2D _rigidbody2D, Animator _animator, Ai _ai)
    {
        rigidbody2D = _rigidbody2D;
        animator = _animator;
        ai = _ai;
    }
    public override NodeState Evaluate()
    {
        var movement = (Vector2)GetData<Vector2>();
        rigidbody2D.velocity = movement * 4;
        if (movement != Vector2.zero)
        {
            animator.SetBool("walk", true);
            //   ai.PV.RPC(nameof(ai.FlipXRPC), RpcTarget.AllBuffered, ai.moveHorizontal); // 재접속시 filpX를 동기화해주기 위해서 AllBuffered
        }
        else
        {
            animator.SetBool("walk", false);
        }
        return base.Evaluate();
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
        if(skillTime <= 0)
        {

        }
        else
        {
            skillTime -= Time.deltaTime;
        }
        photonView.RPC(nameof(punRPC),RpcTarget.All);
        return NodeState.RUNNING;
    }
}