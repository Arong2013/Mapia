using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class VoteUI : MonoBehaviourPunCallbacks
{
    public Text VotingResultText;

    public Transform VoteSlotParent;

    public GameObject VoteSlot;

    VoteManager voteManager;
    private void Awake()
    {
        voteManager = VoteManager.Instance.GetComponent<VoteManager>();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        print(PhotonNetwork.PlayerList.Length);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            int index = i;
            GameObject slot = Instantiate(VoteSlot.gameObject, VoteSlotParent);
            slot.GetComponent<Button>().onClick.AddListener(() => voteManager.CastVote(index));
            string playerName = PhotonNetwork.PlayerList[index].NickName;
            slot.GetComponent<VoteSlot>().SetSlot(playerName);
        }
        print(PhotonNetwork.PlayerList.Length);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = VoteSlotParent.childCount - 1; i >= 0; i--)
        {
            // 자식 오브젝트를 가져와서 삭제
            Destroy(VoteSlotParent.GetChild(i).gameObject);
        }
    }


    public void UpdateVoteUI()
    {
        for (int i = 0; i < VoteSlotParent.childCount; i++)
        {
            if (i < PhotonNetwork.PlayerList.Length)
            {
                string playerName = PhotonNetwork.PlayerList[i].NickName;
                VoteSlotParent.GetChild(i).GetComponent<VoteSlot>().UpdateSlot(voteManager.voteCounts[playerName]);
            }

            if (i < PhotonNetwork.PlayerList.Length)
            {

                //  VoteText[i].text = $"{playerName}: {voteCounts[playerName]}";
                // VoteButtons[i].interactable = true;
            }
            else
            {
                //  VoteText[i].text = "";
                //   VoteButtons[i].interactable = false;
            }
        }
    }
}
