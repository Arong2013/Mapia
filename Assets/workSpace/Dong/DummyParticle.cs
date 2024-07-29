using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyParticle : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float TTime = 0f;

    Vector3 MovePos;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float RandomSize = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector2(RandomSize, RandomSize);
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        TTime += Time.deltaTime;
        spriteRenderer.color = Color.Lerp(Color.white, Color.clear, TTime * 2);

        transform.position = Vector3.Lerp(transform.position, MovePos, TTime /5);

        if(spriteRenderer.color.a == 0)
        {
            Destroy(gameObject);
        }


    }

    public void RandomPos()
    {
        int randomNum = Random.Range(0, 4);

        switch (randomNum)
        {
            case 0:

                MovePos = new Vector3(transform.position.x + 0.1f, transform.position.y);

                break;
                
            case 1:

                MovePos = new Vector3(transform.position.x - 0.1f, transform.position.y);

                break;
                
            case 2:

                MovePos = new Vector3(transform.position.x, transform.position.y + 0.1f);

                break;
                
            case 3:

                MovePos = new Vector3(transform.position.x, transform.position.y - 0.1f);

                break;

            default:
                break;
        }

       // MovePos = new Vector3(transform.position.x + 2, transform.position.y);
    }


}
