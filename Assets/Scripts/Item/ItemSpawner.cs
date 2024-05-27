using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    
        public GameObject DropItem;
    public GameObject[] ItemList;

    float spawnTime =0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime >= 3)
        {
            int num = Random.Range(0, 3);
            int num2 = Random.Range(0, 10);
            Instantiate(ItemList[num], new Vector3(num2,2,0), Quaternion.identity);
            spawnTime = 0;
        }
    }
}
