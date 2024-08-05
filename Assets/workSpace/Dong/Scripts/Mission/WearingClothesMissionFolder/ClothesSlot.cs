using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClothesSlot : MonoBehaviour, IDropHandler
{
    public bool Wear = false;

    public int ID; //�Ծ���ϴ� �� ���� 
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
    }

    public void Initalizing(Transform ParentTransform)
    {
        CurrentID = 100;
        if (transform.childCount >0)
        {
            foreach(Clothes child in transform.GetComponentsInChildren<Clothes>())
            {
                if(child != transform)
                {
                    child.transform.parent = ParentTransform;
                }
                
            }
        }
    }


    
}
