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
    public float requiredHoldTime = 2f; // 2�� ���� ���콺�� Ŭ���ϰ� �־�� ������Ʈ�� ����

    void Update()
    {
        // ���콺�� Ŭ���ǰ� �ִ� ���� �ð��� ����մϴ�.
        if (isMouseHeld)
        {
            mouseHoldTime += Time.deltaTime;
            Debug.Log("�۵�");

            // ���콺�� Ư�� �ð� ���� Ŭ���ϰ� ������ ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            if (mouseHoldTime >= requiredHoldTime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseHeld = true;
        mouseHoldTime = 0f; // ���콺 Ŭ�� ���� �� �ð��� �ʱ�ȭ�մϴ�.
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseHeld = false;
    }

    void OnMouseDown()
    {
        isMouseHeld = true;
        mouseHoldTime = 0f; // ���콺 Ŭ�� ���� �� �ð��� �ʱ�ȭ�մϴ�.
    }

    void OnMouseUp()
    {
        isMouseHeld = false;
    }
}

