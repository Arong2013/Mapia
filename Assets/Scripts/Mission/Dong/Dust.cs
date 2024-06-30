using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dust : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    private bool isMouseHeld = false;
    private float mouseHoldTime = 0f;
    public float requiredHoldTime = 2f; // 2초 동안 마우스를 클릭하고 있어야 오브젝트가 꺼짐

    void Update()
    {
        // 마우스가 클릭되고 있는 동안 시간을 기록합니다.
        if (isMouseHeld)
        {
            mouseHoldTime += Time.deltaTime;
            Debug.Log("작동");

            // 마우스를 특정 시간 동안 클릭하고 있으면 오브젝트를 비활성화합니다.
            if (mouseHoldTime >= requiredHoldTime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseHeld = true;
        mouseHoldTime = 0f; // 마우스 클릭 시작 시 시간을 초기화합니다.
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseHeld = false;
    }

    void OnMouseDown()
    {
        isMouseHeld = true;
        mouseHoldTime = 0f; // 마우스 클릭 시작 시 시간을 초기화합니다.
    }

    void OnMouseUp()
    {
        isMouseHeld = false;
    }
}

