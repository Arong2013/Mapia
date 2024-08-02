using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClothesSlot : MonoBehaviour, IDropHandler
{
    public bool Wear = false;

    public int ID; //입어야하는 옷 정보 
    public int CurrentID = 100;



    Image mysprite;




    public void OnDrop(PointerEventData eventData)
    {
        Wear = true;

        if (ID == WearingClothes.instance.pick.ClothesID)
        {
            WearingClothes.instance.pick.transform.parent = transform;
            WearingClothes.instance.pick.SetZero();
            WearingClothes.instance.pick.UnInitialize();
            CurrentID = WearingClothes.instance.pick.ClothesID;

        }


    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentID = 100;

        //wearingClothes = UiUtils.GetUI<WearingClothes>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
