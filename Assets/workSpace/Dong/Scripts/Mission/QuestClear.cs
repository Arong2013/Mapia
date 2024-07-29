using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestClear : MonoBehaviour, IPointerClickHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        QuestManager.Instance.QuestClear();
        Debug.Log("미션 클리어 퀘스트 패널 닫음");
    }
}
