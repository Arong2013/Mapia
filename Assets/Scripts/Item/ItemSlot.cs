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
    public Inventory inventory;
    Vector2 defaultPosition;
    Transform ParentTransform;
    public ItemType type;

    public void Start()
    {
        //myImage = GetComponent<Image>();
        inventory = Manager.Instance.GetChild<GameManager>().invenTory;
        ParentTransform = transform.parent;
        Debug.Log(ParentTransform.name);
    }

    public void OnBeginDrag(PointerEventData eventData) //�巡�� ����
    {
        transform.SetParent(UiUtils.GetUI<InventoryUI>().transform);
        defaultPosition = GetComponent<RectTransform>().localPosition;
        myImage.raycastTarget = false;
        inventory.DragSlot = GetComponent <ItemSlot>();
        //���� ���� �巡�׸� ������ �� ó����ġ�� ��ǥ�� �����س���
    }

    public void OnDrag(PointerEventData eventData) //�巡������ ��
    {
        //Debug.Log(gameObject.name);
        Vector2 CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = CurrentPos;

    }

    public void OnEndDrag(PointerEventData eventData) //�巡�� ������ ��
    {
        transform.localPosition = defaultPosition;
        transform.SetParent(ParentTransform);
        myImage.raycastTarget = true;
        Insert_Data();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //���� �������� �ִ� ��ġ�� ������ ������ �����ϴ��� üũ�ؼ� �ִٸ� ������ �������
        //���� ���´� �������� ������Ű�� ����
        if(inventory.DragSlot.type == type)
        {
            if (inventory.DragSlot != null)
            {
                if (item != null)
                {
                    Debug.Log("���� �־��" + item.Data.name);
                    var Temp_Data = item;
                    item = inventory.DragSlot.item;
                    inventory.DragSlot.item = Temp_Data;
                    Insert_Data();

                }
                else
                {
                    Debug.Log("�۵��� �ǿ�");
                    item = inventory.DragSlot.item;
                    inventory.DragSlot.item = null;
                    Insert_Data();

                }


            }
            else
            {
                Debug.Log("null�̿���");
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
