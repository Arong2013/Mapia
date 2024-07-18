using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMarking : MonoBehaviour
{
    bool Set = true; 
    SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag("Wall"))
            {
                spriteRenderer.color = Color.red;
                Set = false;
                //return false 함정을 여기 위치에 설치할 수 없다는 것을 알림 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            spriteRenderer.color = Color.white;
            Set = true;
        }
    }

    public bool GetBool()
    {
        return Set;
    }



}
