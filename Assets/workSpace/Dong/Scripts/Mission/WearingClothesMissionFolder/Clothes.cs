using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clothes : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public RectTransform myrect;
    Image myImg;
    public int ClothesID;

    Transform StartParent;
    Transform CurrentParent;

    Vector2 StartPos;

    bool initalize = true;


    void Start()
    {
        myImg = GetComponent<Image>();
        myrect = GetComponent<RectTransform>();
        StartParent = transform.parent;
        StartPos = myrect.anchoredPosition;
        //Debug.Log(StartPos);
        //Debug.Log(StartParent.name);

    }


    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
    public void OnPointerUp(PointerEventData eventData)
    {
       
       //�ӵ��� ������ ���⸻�� EndDrag���ٰ� �־���

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = CurrentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (initalize)
        {
            myrect.anchoredPosition = StartPos;
            myImg.raycastTarget = true;
        }
        WearingClothes.instance.pick = null;
        //SetParentInitalize();
    }

    public void SetID(int num)
    {
        ClothesID = num;
    }

    public int GetID()
    {
        return ClothesID;
    }

    public void SetZero()
    {
        myrect.anchoredPosition = Vector2.zero;
    }
    
    public void SetParentInitalize()
    {
        transform.parent = StartParent;
    }

    public void UnInitialize()
    {
        initalize = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetParentInitalize();
        gameObject.transform.SetAsLastSibling(); //�ش� �޼��带 ���ؼ� Ŭ���� UI�� �� �󿡼� ���� ������ ���̵��� ��ġ�� �� ���� (Ŭ���� ������Ʈ�� ���� ������ ���� ������Ʈ�� ����)
        myImg.raycastTarget = false;
        WearingClothes.instance.pick = this;
    }

    public void RaycastTargetOn()
    {
        myImg.raycastTarget = true;
    }

}
