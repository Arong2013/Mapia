using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        int targetCount = 4;

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
                if (inventory.Items[i] != null)
                {
                    var index = i;
                    slot.GetComponent<ItemSlot>().SetItem(inventory.Items[index]);
                    
                }
                else
                {
                    slot.GetComponent<ItemSlot>().SetItem(null);
                }

                inventorySlots.Add(slot.GetComponent<ItemSlot>());

            }
        }
    }
    public void SetPlayer(Actor player)
    {
        actor = player;
        actor.inventory.ItemChangeActions += UpdateInventoryUI;
    }
}
