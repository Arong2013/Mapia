using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>, IPunObservable
{
    Actor playerController;
    private List<string> jobs = new List<string> { nameof(Chaser), nameof(Dectective), nameof(Sasori), "Healer" };
    PhotonView PV;
    List<Actor> playersData = new List<Actor>();

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
            {
                GameObject[] rootObjects = scene.GetRootGameObjects();
                foreach (GameObject rootObject in rootObjects)
                {
                    GetAllChildObjects(rootObject, allObjects);
                }
            }
        }
        foreach (GameObject obj in allObjects)
        {
            foreach (var playerSettable in obj.GetComponents<IPlayerable>())
            {
                playerSettable.SetPlayer(_char);
            }
        }
    }
    void GetAllChildObjects(GameObject parent, List<GameObject> allObjects)
    {
        allObjects.Add(parent);

        foreach (Transform child in parent.transform)
        {
            GetAllChildObjects(child.gameObject, allObjects);
        }
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
            GameObject gameObject = PhotonNetwork.Instantiate(job, new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
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
    public string CheckData(string text)
    {
        var actorName = playersData.Find(x => x.PV.Owner.NickName == text);
        return actorName.PV.Owner.NickName;
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
