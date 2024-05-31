using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public class VoteManager : MonoBehaviourPunCallbacks
{
    public Dictionary<string, int> voteCounts = new Dictionary<string, int>();
    public Dictionary<string, string> playerVotes = new Dictionary<string, string>();
    private bool isVotingActive = false;
    public VoteUI voteUI;

    string myVoteName;
    
    private void Start()
    {
        voteUI = UiUtils.GetUI<VoteUI>();
    }
    
    public void InitiateVote()
    {
        if (!isVotingActive)
        {
            photonView.RPC("StartVote", RpcTarget.All);
        }
    }

    [PunRPC]
    void StartVote()
    {
        isVotingActive = true;
        voteCounts.Clear();
        playerVotes.Clear();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            voteCounts.Add(player.NickName, 0);
        }
        voteUI.OpenUI();
        voteUI.UpdateVoteUI();
    }

    public void CastVote(int playerIndex)
    {
        if (isVotingActive && playerIndex < PhotonNetwork.PlayerList.Length)
        {
            string votedPlayer = PhotonNetwork.PlayerList[playerIndex].NickName;
            string localPlayerName = PhotonNetwork.LocalPlayer.NickName;

            if (playerVotes.ContainsKey(localPlayerName) && playerVotes[localPlayerName] == votedPlayer)
            {
                photonView.RPC("UnregisterVote", RpcTarget.All, localPlayerName);
            }
            else
            {
                if (!string.IsNullOrEmpty(myVoteName))
                {
                    photonView.RPC("UnregisterVote", RpcTarget.All, localPlayerName);
                }

                photonView.RPC("RegisterVote", RpcTarget.All, localPlayerName, votedPlayer);
            }
        }
    }

    [PunRPC]
    void RegisterVote(string voter, string votedPlayer)
    {
        if (voteCounts.ContainsKey(votedPlayer))
        {
            playerVotes[voter] = votedPlayer;
            voteCounts[votedPlayer]++;
            voteUI.UpdateVoteUI();
        }
    }

    [PunRPC]
    void UnregisterVote(string voter)
    {
        if (playerVotes.ContainsKey(voter))
        {
            string previousVote = playerVotes[voter];
            voteCounts[previousVote]--;
            playerVotes.Remove(voter);
            voteUI.UpdateVoteUI();
        }
    }

    public void EndVote()
    {
        int resultVoteCount = 0;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            resultVoteCount += voteCounts[PhotonNetwork.PlayerList[i].NickName];
            if (resultVoteCount == PhotonNetwork.PlayerList.Length)
            {
                photonView.RPC("ConcludeVote", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void ConcludeVote()
    {
        isVotingActive = false;
        voteUI.gameObject.SetActive(false);
        UiUtils.GetUI<ResultUI>().gameObject.SetActive(true);
        string result = DetermineVoteResult();
        voteUI.VotingResultText.text = result;
        voteUI.VotingResultText.gameObject.SetActive(true);
    }

    string DetermineVoteResult()
    {
        string highestVotedPlayer = "";
        int highestVotes = 0;

        foreach (var vote in voteCounts)
        {
            if (vote.Value > highestVotes)
            {
                highestVotes = vote.Value;
                highestVotedPlayer = vote.Key;
            }
        }

        return highestVotedPlayer != "" ? $"{highestVotedPlayer}가 추방되었습니다." : "추방된 사람이 없습니다.";
    }
}
