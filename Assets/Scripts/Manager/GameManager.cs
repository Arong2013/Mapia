using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>, IPunObservable
{
    private Actor playerController;
    private PhotonView PV;
    private List<string> jobs = new List<string> { nameof(Chaser), nameof(Detective), nameof(Sasori), nameof(DeathNote), nameof(TrapMaker) };
    private List<Actor> playersData = new List<Actor>();

    protected override void Awake()
    {
        base.Awake();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(AssignJob());
    }

    public void AddPlayer(Actor _char)
    {
        playerController = _char;
        List<GameObject> allObjects = new List<GameObject>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
                GetAllChildObjects(scene.GetRootGameObjects(), allObjects);
        }
        foreach (GameObject obj in allObjects)
            foreach (var playerSettable in obj.GetComponents<IPlayerable>())
                playerSettable.SetPlayer(_char);
    }

    private void GetAllChildObjects(GameObject[] rootObjects, List<GameObject> allObjects)
    {
        foreach (GameObject obj in rootObjects)
        {
            allObjects.Add(obj);
            foreach (Transform child in obj.transform)
                GetAllChildObjects(new GameObject[] { child.gameObject }, allObjects);
        }
    }

    IEnumerator AssignJob()
    {
        while (PhotonNetwork.CurrentRoom == null)
            yield return null;

        var availableJobs = new List<string>(jobs);
        var players = PhotonNetwork.CurrentRoom.Players.Values.ToList();
        foreach (var player in players)
        {
            string job = availableJobs[Random.Range(0, availableJobs.Count)];
            availableJobs.Remove(job);
            PV.RPC("SetJob", RpcTarget.AllBuffered, player, job);
            yield return null;
        }
    }

    [PunRPC]
    public void SetJob(Player player, string job)
    {
        if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            GameObject gameObject = PhotonNetwork.Instantiate(job, new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
            PV.RPC(nameof(SetID), RpcTarget.AllBuffered, gameObject.GetPhotonView().ViewID, PhotonNetwork.LocalPlayer.ActorNumber.ToString());
        }
    }

    [PunRPC]
    private void SetID(int viewID, string _ID)
    {
        var actor = PhotonView.Find(viewID).gameObject.GetComponent<Actor>();
        if (actor != null) actor.ID = _ID;
    }

    public void AddActor()
    {
        PV.RPC(nameof(RPCaddActor), RpcTarget.All);
    }

    [PunRPC]
    private void RPCaddActor()
    {
        foreach (var actor in GameObject.FindObjectsOfType<Actor>())
        {
            if (!playersData.Exists(x => x.ID == actor.ID))
                playersData.Add(actor);
        }
    }

    public void ShowObjectToPlayer(string _name, Vector3 _spawnPos)
    {
        PV.RPC(nameof(RPCShowObjectToPlayer), RpcTarget.All, _name, _spawnPos);
    }

    [PunRPC]
    private void RPCShowObjectToPlayer(string _name, Vector3 _spawnPos)
    {
        var foot = PhotonNetwork.Instantiate("foot", _spawnPos, Quaternion.identity).GetComponent<ChaserFoot>();
        foot.playerName = _name;
    }

    public void DestroyGameobject(string _name)
    {
        PV.RPC(nameof(RPCDestroyGameobject), RpcTarget.All, _name);
    }

    [PunRPC]
    private void RPCDestroyGameobject(string _name)
    {
        Destroy(playersData.Find(x => x.ID == _name).gameObject);
    }

    public string CheckData(string text)
    {
        return playersData.Find(x => x.PV.Owner.NickName == text)?.PV.Owner.NickName;
    }

    public List<string> GetNickNames()
    {
        return playersData.Select(player => player.PV.Owner.NickName).ToList();
    }
    public Camp CheckLocalPlayerSide()
    {
        var localPlayer = playersData.FirstOrDefault(player => player.PV.IsMine);
        return localPlayer?.PlayerSide ?? Camp.None;
    }

    public override void OnJoinedRoom() { }

    public override void OnDisconnected(DisconnectCause cause) { }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
}
