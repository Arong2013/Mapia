using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

public class PlayerController : Ai
{
    RpcManager rpcManager;
    
    private void OnMouseDown()
    {
       // GameManager.Instance.voteManager.InitiateVote();
        PV.RPC("Invisibility", RpcTarget.All);
    }

    private void Start()
    {
        rpcManager = GetComponent<RpcManager>();
    
    }
}
