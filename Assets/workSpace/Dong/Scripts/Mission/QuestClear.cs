using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestClear : MonoBehaviour, IPointerClickHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        QuestManager.Instance.QuestClear();
        Debug.Log("�̼� Ŭ���� ����Ʈ �г� ����");
    }
}
