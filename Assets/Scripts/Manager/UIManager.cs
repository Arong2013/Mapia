using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject Inventory;

     public T GetUI<T>(string _name)
       where T : MonoBehaviour
    {
       T component = GameObject.Find(_name).GetComponent<T>();
       if(component)
       {
         return component;
       }
       else
       {
          Debug.Log("UI매니저의 하위풀더에 존재하지 않는 컴포넌트 입니다");
       }    
       return null;
    }
}
