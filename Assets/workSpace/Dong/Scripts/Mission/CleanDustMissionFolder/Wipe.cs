using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Wipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Vector3 StartPos;
    Vector3 CheckPos;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    public void Initializing()
    {
        transform.position = StartPos;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        CheckPos = new Vector3(0, 0, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //오브젝트 위치 제한 걸어줘야 함
        Vector3 CheckPoss = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckPos = new Vector3(CheckPoss.x, CheckPoss.y,0);
        transform.position = CheckPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(CheckPos);
    }
}
