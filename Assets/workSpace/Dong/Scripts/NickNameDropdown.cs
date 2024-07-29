using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System.Linq;

public class NickNameDropdown : MonoBehaviour
{
    TMP_Dropdown dropdown;
    public TMP_InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(WriteText);
    }

    // Update is called once per frame
    void Update()
    {
        DropdownInitalize();
    }

    void DropdownInitalize() //��� ����ȭ�� ��������ϴ°� �����ϱ� �ϴ� ������Ʈ�� ����
    {
        List<string> names = new List<string>();
        names.Add("Choose");

        foreach (var name in GameManager.Instance.GetNickNames()) //�ٽ��ؾ���
        {
            names.Add(name);
        }

        dropdown.ClearOptions();
        
        dropdown.AddOptions(names);
        
        dropdown.RefreshShownValue();
        //Debug.Log(PhotonNetwork.CountOfPlayers.ToString());
    }

    public void WriteText(int num)
    {
        inputField.text = null;
        inputField.text = dropdown.options[num].text;
    }



}
