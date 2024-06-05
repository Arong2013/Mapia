using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;
public class GameManager : Singleton<GameManager>
{
    public GameObject RespawnPanel;

    public VoteManager voteManager;

    public Inventory invenTory;

    private void Start()
    {
    
        voteManager = GetComponent<VoteManager>();
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
        RespawnPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        RespawnPanel.SetActive(false);
    }
}
