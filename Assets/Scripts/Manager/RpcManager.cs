using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RpcManager : MonoBehaviourPunCallbacks, IPunObservable
{
    PlayerController playerController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    [PunRPC]
    public void Invisibility()
    {
        if(playerController.PV.IsMine)
        {
            playerController.SR.color = new Color(0,0,0,0);
        }   
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
