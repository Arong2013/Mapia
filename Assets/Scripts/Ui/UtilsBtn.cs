using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UtilsBtn : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image Icon;
    [SerializeField] private Action btnAction;

    public void OnPointerDown(PointerEventData eventData)
    {
       btnAction?.Invoke();
    }

    public void SetButtonDown(Sprite IconImage)
    {
        if (Icon != null)
        {
            Icon.sprite = IconImage;
        }
    }

    // 버튼의 상태를 초기화하는 메서드를 추가할 수 있습니다.
    public void ResetButton()
    {
        if (Icon != null)
        {
            // Icon의 색상을 원래대로 복원합니다.
            Icon.color = Color.white;
        }
    }

    // Action을 설정하는 메서드를 추가할 수 있습니다.
    public void SetButtonAction(Action action)
    {
        btnAction = action;
    }
}
