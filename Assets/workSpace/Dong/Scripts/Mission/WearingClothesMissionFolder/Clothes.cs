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
       
       //속도가 더빨라서 여기말고 EndDrag에다가 넣어줌

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
        gameObject.transform.SetAsLastSibling(); //해당 메서드를 통해서 클릭한 UI를 씬 상에서 가장 맨위에 보이도록 배치할 수 있음 (클릭한 오브젝트를 가장 마지막 하위 오브젝트로 만듬)
        myImg.raycastTarget = false;
        WearingClothes.instance.pick = this;
    }

    public void RaycastTargetOn()
    {
        myImg.raycastTarget = true;
    }

}
