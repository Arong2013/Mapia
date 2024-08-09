using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnderBarUI : MonoBehaviour, IPlayerable
{
    Actor actor;
    BaseStats healthStats => actor.GetStatComponent<BaseStats>();
    Inventory inventory => actor.inventory;

    [SerializeField] Transform HeartParent, InventoryParent;
    [SerializeField] Image CharIcon;
    [SerializeField] GameObject heartSlot, inventorySlot;

    private List<Image> hearts = new List<Image>();
    private List<ItemSlot> inventorySlots = new List<ItemSlot>();


    void UpdataUI()
    {
        UpdateHeartUI();
        UpdateInventoryUI();
    }
    void UpdateHeartUI()
    {
        int targetCount = (int)healthStats.maxHp.Value;
        int fillAmount = (int)healthStats.curHp.Value;

        if (hearts.Count != targetCount)
        {
            foreach (Transform child in HeartParent)
            {
                Destroy(child.gameObject);
            }
            hearts.Clear();

            for (int i = 0; i < targetCount; i++)
            {
                GameObject slot = Instantiate(heartSlot, HeartParent);
                hearts.Add(slot.GetComponent<Image>());
            }
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].fillAmount = (fillAmount > i) ? 1 : 0;
        }
    }

    void UpdateInventoryUI()
    {
        int targetCount = 3;

        if (inventorySlots.Count != targetCount)
        {
            foreach (Transform child in InventoryParent)
            {
                Destroy(child.gameObject);
            }
            inventorySlots.Clear();


            for (int i = 0; i < targetCount; i++)
            {
                GameObject slot = Instantiate(inventorySlot, InventoryParent);
                if (i < inventory.Items.Count && inventory.Items[i] != null)
                {
                    slot.GetComponent<ItemSlot>().SetItem(inventory.Items[i]);
                }
                else
                {
                    
                    slot.GetComponent<ItemSlot>().SetItem(null);
                }
                inventorySlots.Add(slot.GetComponent<ItemSlot>());
            }

        }
        SetNowItem(actor);

    }
    public void SetPlayer(Actor player)
    {
        actor = player;
        actor.inventory.ItemChangeActions += UpdateInventoryUI;
    }

    public void SetNowItem(Actor player)
    {
        actor = player;
        foreach (var inventoryslot in inventorySlots)
        {
            inventoryslot.SlotClicked += actor.inventory.ItemChoiceActions;
            Debug.Log(inventoryslot.item.Data.name + "1111111");

        }
        
    }

    public void SlotClicked()
    {

    }

}
