using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviourPunCallbacks
{
    public Inventory invenTory;
    private void Start()
    {
        invenTory = GetComponent<Inventory>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Spawn();
    }

    public override void OnJoinedRoom()
    {

    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Bullet")) GO.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.All);
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
    }
}
