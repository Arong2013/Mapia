using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClickPlace : MonoBehaviour, IPointerClickHandler
{
    int Click_Num = 0; //클릭한 횟수 체크

    //public GameObject Particle;

    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Instantiate(Particle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    //    }
    //}


    public void OnPointerClick(PointerEventData eventData)
    {

       

        //Debug.Log("체크");
        Click_Num++;
    }

    public int GetClick_num()
    {
        return Click_Num;
    }
    public void IntializeClick_Num()
    {
        Click_Num = 0;
    }

}
