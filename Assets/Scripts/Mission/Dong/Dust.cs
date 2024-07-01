using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dust : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool Erased = false;

    void Start()
    {

    }


    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }


    float albedo = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wipe"))
        {
            Debug.Log("닿음");
            Image dust_img = GetComponent<Image>();
            if (albedo > 0)
            {
                albedo -= 0.1f;
                dust_img.color = new Color(0, 0, 0, albedo);
            }
            else
            {
                albedo = 0;
                dust_img.color = new Color(0, 0, 0, albedo);
                Erased = true;
                //따로 체크해주는 코드 넣어주기
            }



        }
    }


}

