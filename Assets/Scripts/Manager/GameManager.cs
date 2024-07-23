using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : Singleton<GameManager>, IPunObservable
{
    public Inventory invenTory;
 
    private List<string> jobs = 
        new List<string> { nameof(Chaser), nameof(Dectective), nameof(Sasori), nameof(Trap_Maker), nameof(DeathNote) };
    PhotonView PV;
    List<Actor> playersData = new List<Actor>();

    protected override void Awake()
    {
        base.Awake();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        invenTory = GetComponent<Inventory>();
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(AssignJob());
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }
    IEnumerator AssignJob()
    {
        while (PhotonNetwork.CurrentRoom == null)
            yield return null;
        var availableJobs = new List<string>(jobs);
        if (PhotonNetwork.IsMasterClient)
        {
            var counts = PhotonNetwork.CurrentRoom.PlayerCount;
            var list = PhotonNetwork.CurrentRoom.Players.Values.ToList();
            while (counts > 0)
            {
                string job = availableJobs[Random.Range(0, availableJobs.Count)];
                availableJobs.Remove(job);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("SetJob", RpcTarget.AllBuffered, list[counts - 1], job);
                counts--;
                yield return null;
            }
        }
        yield return null;
    }

    [PunRPC]
    public void SetJob(Player player, string job)
    {
        if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            GameObject gameObject = PhotonNetwork.Instantiate("TrapSetter", new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
            photonView.RPC(nameof(SetID), RpcTarget.AllBuffered, gameObject.GetPhotonView().ViewID, PhotonNetwork.LocalPlayer.ActorNumber.ToString());
        }
    }
    [PunRPC]
    void SetID(int viewID, string _ID)
    {
        GameObject gameObject = PhotonView.Find(viewID).gameObject;
        Actor actor = gameObject.GetComponent<Actor>();
        if (actor != null)
        {
            actor.ID = _ID;
        }
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

            if (!playersData.Exists(x => x.ID == cunID))
            {
                playersData.Add(cunActor);
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

    public void DestroyGameobject(string _name)
    {
        
        PV.RPC(nameof(RPCDestroyGameobject), RpcTarget.All, _name);
    }

    [PunRPC]
    void RPCDestroyGameobject(string _name)
    {
        Destroy(playersData.Find(x => x.ID == _name));
    }


    public Camp CheckLocalPlayerSide()
    {
        foreach (var player in playersData)
        {
            if (player.PV.IsMine)
            {
                return player.PlayerSide;
            }
        }

        return Camp.None;

    }



    public string CheckData(string text)
    {
        var actorName = playersData.Find(x => x.PV.Owner.NickName == text);
        return actorName.PV.Owner.NickName;
    }

    public List<string> GetNickNames()
    {
        List<string> nicknames = new List<string>();

        foreach(var name in playersData)
        {
            nicknames.Add(name.PV.Owner.NickName);
        }
        return nicknames;


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
