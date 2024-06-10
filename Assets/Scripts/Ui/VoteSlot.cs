using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using TMPro;

public class VoteSlot : MonoBehaviourPunCallbacks
{
    [SerializeField] string slotName;
    [SerializeField] TextMeshProUGUI countTest;

    public void SetSlot(string _name)
    {
        slotName = _name;
    }
    public void UpdateSlot(int _voteCount)
    {
        countTest.text = _voteCount.ToString();
    }
}
