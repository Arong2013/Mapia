using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;
using System;

public static class AIUtils
{
    public class MoveNode : Node
    {
        public override NodeState Evaluate()
        {
            var movement = (Vector2)GetDeta<Vector2>();
            NodeAI.RB.velocity = movement * 4;
            if (movement != Vector2.zero)
            {
                NodeAI.AN.SetBool("walk", true);
                NodeAI.PV.RPC(nameof(NodeAI.FlipXRPC), RpcTarget.AllBuffered, NodeAI.moveHorizontal); // 재접속시 filpX를 동기화해주기 위해서 AllBuffered
            }
            else
            {
                NodeAI.AN.SetBool("walk", false);
            }
            return base.Evaluate();
        }
    }
}