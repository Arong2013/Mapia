using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private int count;
    public Item item;
    public Image myImage;
    public InventoryUI inventoryUI;
    Vector2 defaultPosition;
    Transform ParentTransform;
    public ItemType type;

    public void Start()
    {
        //myImage = GetComponent<Image>();
        inventoryUI = UiUtils.GetUI<InventoryUI>();
        ParentTransform = transform.parent;
        Debug.Log(ParentTransform.name);
    }

    public void OnBeginDrag(PointerEventData eventData) //드래그 시작
    {
        transform.SetParent(UiUtils.GetUI<InventoryUI>().transform);
        defaultPosition = GetComponent<RectTransform>().localPosition;
        myImage.raycastTarget = false;
        inventoryUI.DragSlot = GetComponent<ItemSlot>();
        //가장 먼저 드래그를 시작할 때 처음위치의 좌표를 저장해놓음
    }

    public void OnDrag(PointerEventData eventData) //드래그중일 때
    {
        //Debug.Log(gameObject.name);
        Vector2 CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = CurrentPos;

    }

    public void OnEndDrag(PointerEventData eventData) //드래그 끝났을 때
    {
        transform.localPosition = defaultPosition;
        transform.SetParent(ParentTransform);
        myImage.raycastTarget = true;
        Insert_Data();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //현재 아이템이 있는 위치에 아이템 슬롯이 존재하는지 체크해서 있다면 변경을 해줘야함
        //지금 상태는 아이템을 복제시키는 거임
        if (inventoryUI.DragSlot.type == type)
        {
            if (inventoryUI.DragSlot != null)
            {
                if (item != null)
                {
                    Debug.Log("뭐가 있어요" + item.Data.name);
                    var Temp_Data = item;
                    item = inventoryUI.DragSlot.item;
                    inventoryUI.DragSlot.item = Temp_Data;
                    Insert_Data();

                }
                else
                {
                    Debug.Log("작동이 되요");
                    item = inventoryUI.DragSlot.item;
                    inventoryUI.DragSlot.item = null;
                    Insert_Data();

                }


            }
            else
            {
                Debug.Log("null이에요");
            }
        }
    }

    void Insert_Data()
    {
        if (item != null)
        {
            myImage.sprite = item.Data.IconSprite;
        }
        else
        {
            myImage.sprite = null;
        }

    }


    public void SetItem(Item data)
    {
        item = data;
        type = item.Data.Itemtype;
        if (item != null)
            myImage.sprite = item.Data.IconSprite;
        else
            myImage.sprite = null;

    }


}