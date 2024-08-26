using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public event Action SkillSlotClicked;


    //아이템 슬롯 처럼 만들면 될 것 같음

    //public void SetSkill(Skill _skill)
    //{
    //    if(_skill != null )
    //    {
    //        //굳이 저장도 안해줘도 될 것 같긴함 그냥 스킬 슬롯은 오로지 실행이 됬는지 확인만 하는 그런 공간으로 사용될 것 가틍ㅁ
            

    //    }


    //    //if (_item != null)
    //    //{
    //    //    Debug.Log(_item.Data.name);

    //    //    item = _item;
    //    //    iconImage.sprite = item?.Data.IconSprite ?? OrizinImage;
    //    //    iconImage.color = iconImage.sprite != null ? Color.white : new Color(0, 0, 0, 0);
    //    //    _amountText.text = item?.Amount.ToString() ?? "";
    //    //}

    //}



    public void OnPointerDown(PointerEventData eventData)
    {
        SkillSlotClicked.Invoke();
        Image img = GetComponent<Image>();
        img.color = Color.red;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Image img = GetComponent<Image>();
        img.color = Color.white;



        //var handler = typeof(IPointerUpHandler);
        //if (touchDic.ContainsKey(handler))
        //{
        //    touchDic[handler].Invoke();
        //}
        //Image img = GetComponent<Image>();
        //img.color = Color.white;
    }



}
