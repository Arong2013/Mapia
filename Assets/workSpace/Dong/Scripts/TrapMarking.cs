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

    private void Update()
    {
        FollowingMouse();
    }

    void FollowingMouse() // 마우스 따라가요
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(MousePos.x, MousePos.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag("Wall") || collision.CompareTag("Ground") || collision.CompareTag("Trap"))
            {
                spriteRenderer.color = Color.red;
                Set = false;
                //return false 함정을 여기 위치에 설치할 수 없다는 것을 알림 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground") || collision.CompareTag("Trap"))
        {
            spriteRenderer.color = Color.green;
            Set = true;
        }
    }

    public bool GetBool()
    {
        return Set;
    }



}
