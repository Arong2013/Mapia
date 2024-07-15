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
        //여기에서 플레이어 ID 작동하도록 만들어주기
        //해당 닉네임을 가진 플레이어가 있는지 먼저 체크해주고
        //그 다음 그 플레이어의 ID를 가져와서 아래 메서드를 실행하도록


        string id_data = CheckData(text);

        if(id_data != null) 
        {
            GameManager.Instance.DestroyGameobject(id_data);
            Write.text = null;
        }
        else
        {
            Debug.Log("에러 값을 찾을 수 없습니다.");
            Write.text = null;
        }
        
        //Debug.Log(text + "를 적음");
       
    }

    string CheckData(string text)
    {
        //플레이어 ID 확인했을 때 닉네임도 똑같이 확인해야함
        Debug.Log(GameManager.Instance.CheckData(text));
        return GameManager.Instance.CheckData(text);
    }


}
