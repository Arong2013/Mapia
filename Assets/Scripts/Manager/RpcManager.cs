using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RpcManager : Singleton<RpcManager>, IPunObservable
{
    PhotonView PV;
    protected override void Awake()
    {
        base.Awake();
        PV = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    public void Invisibility(Actor actor)
    {
        PV.RPC(nameof(RPCInvisibility), RpcTarget.All, actor);
    }
    [PunRPC]
    public void RPCInvisibility(Actor actor)
    {
        if (actor.PV.IsMine)
        {
            actor.SR.color = new Color(0, 0, 0, 0);
        }
    }

}
