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

    //해당 스크립트를 통해 이 스크립트를 가지고 있는 오브젝트를 클릭했을 때 게임상에서 가장 최상단에 뜨도록 해줍니다.
    public void OnPointerUp(PointerEventData eventData)
    {



        //throw new System.NotImplementedException();
    }
}
