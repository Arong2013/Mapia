using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBtns : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
     Image Icon;

    Dictionary<IEventSystemHandler,Action<PointerEventData>> TouchDic = new Dictionary<IEventSystemHandler, Action<PointerEventData>>();

   
    public void AddPointer<T>(Action<PointerEventData> _action) where T : IEventSystemHandler
    {
        T _eventSystemHandler = transform.GetComponent<T>();
        if (TouchDic.ContainsKey(_eventSystemHandler))
            TouchDic[_eventSystemHandler] = _action;
        else
            TouchDic.Add(_eventSystemHandler, _action);
    }

    public void RemovePointer(Action<PointerEventData> _action)
    {
        List<IEventSystemHandler> keysToRemove = new List<IEventSystemHandler>();
        foreach (var kvp in TouchDic)
        {
            if (kvp.Value.Equals(_action))
            {
                keysToRemove.Add(kvp.Key);
            }
        }

        foreach (var key in keysToRemove)
        {
            TouchDic.Remove(key);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TouchDic.ContainsKey(this))
        {
            TouchDic[this](eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
