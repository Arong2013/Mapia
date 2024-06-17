using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    public T GetChild<T>()
       where T : MonoBehaviour
    {
        T component = null;
        component = gameObject.GetComponentInChildren<T>(true);
        return component;
    }
}
