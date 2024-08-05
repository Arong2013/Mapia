using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Detective : Actor, IPointerClickHandler
{
    bool isMoveable = true;
    public Text ActorText;
    //public string Actor_What;

    public override void Awake()
    {
        base.Awake();
        statComponents.Add(new MovementStats());
    }

    protected override void Start()
    {
        base.Start();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PV.IsMine)
        {
            //ActorText.text = Actor_What;
        }
    }

    public override void Move()
    {
        if (isMoveable)
        {
            RB.velocity = movement;
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
