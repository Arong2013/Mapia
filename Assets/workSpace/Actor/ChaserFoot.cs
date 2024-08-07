using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChaserFoot : MonoBehaviourPunCallbacks, IPunObservable
{
    public string playerName;
    PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Invoke(nameof(Dead),3f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PhotonView>().IsMine && other.TryGetComponent<Chaser>(out Chaser chaser))
        {
            chaser.AddKillCount(playerName);
            PV.RPC("DestroyRPC", RpcTarget.All);
        }
    }
    void Dead()
    {
        PV.RPC("DestroyRPC", RpcTarget.All);
    }

    [PunRPC]
    void DestroyRPC() => Destroy(this.gameObject);

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
