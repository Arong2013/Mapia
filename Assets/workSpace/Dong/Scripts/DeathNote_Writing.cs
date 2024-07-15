using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathNote_Writing : MonoBehaviour
{
    TMP_InputField Write;

    private void Awake()
    {
        Write = GetComponent<TMP_InputField>();
        Write.onEndEdit.AddListener(GetWrite);
    }

    private void Update()
    {
        
    }


    void GetWrite(string text)
    {
        //���⿡�� �÷��̾� ID �۵��ϵ��� ������ֱ�
        //�ش� �г����� ���� �÷��̾ �ִ��� ���� üũ���ְ�
        //�� ���� �� �÷��̾��� ID�� �����ͼ� �Ʒ� �޼��带 �����ϵ���


        string id_data = CheckData(text);

        if(id_data != null) 
        {
            GameManager.Instance.DestroyGameobject(id_data);
            Write.text = null;
        }
        else
        {
            Debug.Log("���� ���� ã�� �� �����ϴ�.");
            Write.text = null;
        }
        
        //Debug.Log(text + "�� ����");
       
    }

    string CheckData(string text)
    {
        //�÷��̾� ID Ȯ������ �� �г��ӵ� �Ȱ��� Ȯ���ؾ���
        Debug.Log(GameManager.Instance.CheckData(text));
        return GameManager.Instance.CheckData(text);
    }


}
