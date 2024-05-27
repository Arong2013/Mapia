using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public ItemData data;
    public Image ItemImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(ItemData item)
    {
        data = item;
        ItemImage.sprite = data.IconSprite;
    }

    public void ResetData()
    {
        data = null;
        ItemImage.sprite = null;
    }

}
