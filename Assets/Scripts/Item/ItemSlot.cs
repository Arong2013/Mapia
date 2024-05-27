using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Inventory[] inventories;
    public int slotCount = 6;
    public ItemData Test;
    // Start is called before the first frame update
    void Start()
    {
        inventories = gameObject.GetComponentsInChildren<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemKeep(ItemData item) //���߿� ������ ������ �÷��̾ �������ʿ��� �޾����� ����� �޼���
    {
        for(int i=0; i<slotCount; i++) 
        {
            if (inventories[i].data == null)
            {
                inventories[i].SetData(item);
                return; //�������� �����ؼ� �ٷ� �޼��� ��������
            }
        }
    }

    public void Itemstorage() //�׽�Ʈ�� 
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (inventories[i].data == null)
            {
                inventories[i].SetData(Test);
                return; //�������� �����ؼ� �޼��� ����
            }

        }

    }

    public void UseItem() //�׽�Ʈ��222
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (inventories[i].data != null)
            {
                //�ش� ��ġ�� �������� ����ϰ� Ư�� ȿ���� ��
                inventories[i].ResetData();
                return; //�������� ����ؼ� �޼��� ����
            }

        }
    }

}
