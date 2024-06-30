using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Wire : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    public RectTransform endpoint; // 전선의 연결 목표 지점
    public Image wireImage; // 전선 이미지

    private Vector3 initialPosition;
    private Vector2 initialSize;
    private GraphicRaycaster graphicRaycaster;

    private bool isconnect;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = rectTransform.position;
        initialSize = rectTransform.sizeDelta;
        rectTransform.pivot = new Vector2(0, 0.5f); // 전선의 시작점을 고정
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        wireImage.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 startPoint = rectTransform.position;
        Vector2 endPoint = eventData.position;

        // 왼쪽으로 이동하지 않도록 제한
        if (endPoint.x < startPoint.x)
        {
            endPoint.x = startPoint.x;
        }

        float distance = Vector2.Distance(rectTransform.position, eventData.position);
        float angle = Vector2.SignedAngle(Vector2.right, endPoint - startPoint);

        rectTransform.sizeDelta = new Vector2(distance * (1 / canvas.transform.localScale.x), rectTransform.sizeDelta.y);
        rectTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // GraphicRaycaster를 통해 레이캐스트 수행
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject == endpoint.gameObject)
            {
                isconnect = true;
                Debug.Log("Wire connected!");
                break;
            }
        }
        if (!isconnect)
        {
            rectTransform.sizeDelta = initialSize;
            rectTransform.position = initialPosition;
            rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
