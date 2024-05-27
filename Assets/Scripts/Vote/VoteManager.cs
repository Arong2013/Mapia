using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class VoteManager : MonoBehaviourPun
{
    public Text voteResultText;
    public Button[] voteButtons;
    public Text[] voteCounts;
    public GameObject voteUIPanel;

    private Dictionary<string, int> voteDict = new Dictionary<string, int>();
    private PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();

        foreach (Button btn in voteButtons)
        {
            btn.onClick.AddListener(() => OnVoteButtonClicked(btn));
        }

        foreach (Text voteCount in voteCounts)
        {
            voteCount.text = "0";
            voteDict.Add(voteCount.name, 0);
        }
    }

    void OnVoteButtonClicked(Button btn)
    {
        string playerName = btn.name;
        PV.RPC("RPC_HandleVote", RpcTarget.All, playerName);
    }

    [PunRPC]
    void RPC_HandleVote(string playerName)
    {
        if (voteDict.ContainsKey(playerName))
        {
            voteDict[playerName]++;
            UpdateVoteCounts();
        }
    }

    void UpdateVoteCounts()
    {
        foreach (Text voteCount in voteCounts)
        {
            voteCount.text = voteDict[voteCount.name].ToString();
        }
    }

    public void ShowVoteResults()
    {
        PV.RPC("RPC_ShowVoteResults", RpcTarget.All);
    }

    [PunRPC]
    void RPC_ShowVoteResults()
    {
        int maxVotes = 0;
        string votedPlayer = "";

        foreach (var vote in voteDict)
        {
            if (vote.Value > maxVotes)
            {
                maxVotes = vote.Value;
                votedPlayer = vote.Key;
            }
        }

        voteResultText.text = votedPlayer + " has been voted out!";
    }
}
