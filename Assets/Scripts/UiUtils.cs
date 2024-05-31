using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UiUtils
{

    public static T GetUI<T>(string _name = null) where T : MonoBehaviour
    {
        T component = null;
        if (component == null)
        {
            component = FindInCanvasChildren<T>();
        }
        else if(component == null)
        {
            Debug.Log(component.name + " found in the current scene.");

        }
        return component;
    }

    // "캔버스" 오브젝트의 자식 중에서 컴포넌트를 찾는 함수
    private static T FindInCanvasChildren<T>() where T : MonoBehaviour
    {
        T component = null;
        GameObject canvas = GameObject.Find("Canvas");

        if (canvas != null)
        {
            component = canvas.GetComponentInChildren<T>(true);
        }

        return component;
    }
}
