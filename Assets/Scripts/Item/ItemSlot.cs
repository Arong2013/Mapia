using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Item item;

    [SerializeField] Sprite OrizinImage;
    [SerializeField] Image iconImage;
    [SerializeField] private TextMeshProUGUI _amountText;

    private Dictionary<Type, Action> touchDic = new Dictionary<Type, Action>();

    private void Awake()
    {
        OrizinImage = iconImage.sprite;
    }

    public void SetItem(Item _item)
    {
        item = _item;
        iconImage.sprite = item?.Data.IconSprite ?? OrizinImage;
        iconImage.color = iconImage.sprite != null ? Color.white : new Color(0, 0, 0, 0);
        _amountText.text = item?.Amount.ToString() ?? "";
    }
    private void Update()
    {
        if (item != null)
        {
            SetItem(null);
        }
    }
    public void AddPointer<T>(Action action) where T : IEventSystemHandler
    {
        var handler = typeof(T);
        if (touchDic.ContainsKey(handler))
        {
            touchDic[handler] = action;
        }
        else
        {
            touchDic.Add(handler, action);
        }
    }
    public void RemovePointer<T>() where T : IEventSystemHandler
    {
        var handler = typeof(T);
        if (touchDic.ContainsKey(handler))
        {
            touchDic.Remove(handler);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var handler = typeof(IPointerDownHandler);
        if (touchDic.ContainsKey(handler))
        {
            touchDic[handler].Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var handler = typeof(IPointerUpHandler);
        if (touchDic.ContainsKey(handler))
        {
            touchDic[handler].Invoke();
        }
    }
}
