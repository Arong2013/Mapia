using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDustQuest : MonoBehaviour
{
    public int Count = 3;
    public List<Vector2> Dust_Pos;
    public GameObject DustPrefab; //이미 3개가 생성되어있다면 위치만 변경시켜주도록 함

    
    // Start is called before the first frame update
    void Start()
    {
        //for(int i=0; i<Count; i++)
        //{
            Dust_Pos.Add(new Vector2(0, 0));
        Dust_Pos.Add(new Vector2(100, -100));
        Dust_Pos.Add(new Vector2(-100, 100));


        for (int i = 0; i < Count;i++)
        {
            GameObject Dust = Instantiate(DustPrefab,transform);
            RectTransform dust_rect = Dust.GetComponent<RectTransform>();

            dust_rect.anchoredPosition = Dust_Pos[i];
            
            Debug.Log(dust_rect.rect);
        }

        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
