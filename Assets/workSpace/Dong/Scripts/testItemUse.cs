using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testItemUse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemMoving(int check)
    {
        switch(check)
        {
            case 0:
                transform.position = new Vector2(transform.position.x+10,transform.position.y);
                break;

            case 1:
                transform.position = new Vector2(transform.position.x, transform.position.y+10);
                break;

            case 2:
                transform.position = new Vector2(transform.position.x -10, transform.position.y);
                break;

            case 3:
                transform.position = new Vector2(transform.position.x, transform.position.y-10);
                break;
        }
    }
    


}
