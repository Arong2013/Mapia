using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clothes : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform myrect;

    void Awake()
    {
        myrect = GetComponent<RectTransform>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //myrect.anchoredPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
        gameObject.transform.SetAsLastSibling();

        //throw new System.NotImplementedException();
    }

    //�ش� ��ũ��Ʈ�� ���� �� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� Ŭ������ �� ���ӻ󿡼� ���� �ֻ�ܿ� �ߵ��� ���ݴϴ�.
    public void OnPointerUp(PointerEventData eventData)
    {



        //throw new System.NotImplementedException();
    }
}
