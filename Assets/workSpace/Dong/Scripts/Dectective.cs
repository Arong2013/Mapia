using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dectective : Actor, IPointerClickHandler
{
    string hello = "Hello World!";
    public Text ActorText;
    public string Actor_What;
    RpcManager rpcManager;

    private void OnMouseDown()
    {
        // GameManager.Instance.voteManager.InitiateVote();
        //PV.RPC("Invisibility", RpcTarget.All);
    }

    private void Start()
    {
        rpcManager = GetComponent<RpcManager>();
        //ActorText.text = hello;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PV.IsMine)
        {
            ActorText.text = Actor_What;
        }
    }

    public override void Move()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
        RB.velocity = movement * 4f;
       // CallAct<MovementNode>(new MovementNode(movement));
        PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
    }
}
