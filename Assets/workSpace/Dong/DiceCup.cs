using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCup : MonoBehaviour
{
    float CupOpenTime = 0;

    public void CupMethod()
    {

        StartCoroutine(OpenCup());


    }

    IEnumerator OpenCup()
    {
        
       // while(CupOpenTime < 5f)
      //  {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5f /* Time.deltaTime*/);
        //    CupOpenTime += Time.deltaTime;
       // }

        

        yield return null;

    }



}
