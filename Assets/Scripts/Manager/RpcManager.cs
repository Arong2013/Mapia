using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RpcManager : MonoBehaviourPunCallbacks, IPunObservable
{
    Actor actor;
    private void Awake()
    {
        actor = GetComponent<Actor>();
    }
    // [PunRPC]
    // public void Invisibility()
    // {
    //     if(playerController.pv.IsMine)
    //     {
    //         playerController.SR.color = new Color(0,0,0,0);
    //     }   
    // }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
