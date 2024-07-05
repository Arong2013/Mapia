using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : Singleton<GameManager>, IPunObservable
{
    public Inventory invenTory;
    private List<string> jobs = new List<string> { "Warrior", "Mage", "Archer", "Healer" };
    private List<string> availableJobs;

    PhotonView PV;

    Dictionary<string, Actor> playersData = new Dictionary<string, Actor>();

    protected override void Awake()
    {
        base.Awake();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        invenTory = GetComponent<Inventory>();
        availableJobs = new List<string>(jobs);
        AssignJob();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public void AssignJob()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (availableJobs.Count > 0)
            {
                string job = availableJobs[Random.Range(0, availableJobs.Count)];
                availableJobs.Remove(job);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("SetJob", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer, job);
            }
            else
            {
                Debug.LogWarning("No more jobs available to assign.");
            }
        }
    }

    [PunRPC]
    public void SetJob(Player player, string job)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Assigned Job: " + job);
            Spawn("Player");
        }
    }
    public void Spawn(string _name)
    {
        PhotonNetwork.Instantiate(_name, new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
    }

    public void AddActor()
    {
        photonView.RPC(nameof(RPCaddActor), RpcTarget.All);
    }
    [PunRPC]
    void RPCaddActor()
    {
        Actor[] cunActors = GameObject.FindObjectsOfType<Actor>();
        for (int i = 0; i < cunActors.Length; i++)
        {
            var cunActor = cunActors[i];
            var cunID = cunActor.ID;

            if (!playersData.ContainsKey(cunID))
            {
                playersData.Add(cunID, cunActor);
            }
        }
    }

    public void ShowObjectToPlayer(string _name, Vector3 _spawnPos)
    {
        PV.RPC(nameof(RPCShowObjectToPlayer), RpcTarget.All, _name, _spawnPos);
    }
    [PunRPC]
    void RPCShowObjectToPlayer(string _name, Vector3 _spawnPos)
    {
        GameObject gameObject = PhotonNetwork.Instantiate("foot", _spawnPos, Quaternion.identity);
        gameObject.GetComponent<ChaserFoot>().playerName = _name;
    }

    public override void OnJoinedRoom()
    {

    }
    public override void OnDisconnected(DisconnectCause cause)
    {

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
