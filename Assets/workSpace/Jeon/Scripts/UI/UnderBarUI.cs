using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderBarUI : MonoBehaviour, IPlayerable
{
    Actor actor;
    HealthStats healthStats => actor.GetStatComponent<HealthStats>();
    Inventory inventory => actor.inventory;

    [SerializeField] Transform HeartParent, InventoryParent;
    [SerializeField] Image CharIcon;
    [SerializeField] GameObject heartSlot, inventorySlot;

    private List<Image> hearts = new List<Image>();
    private List<Image> inventorySlots = new List<Image>();
    void Update()
    {
        if (actor != null)
        {
            UpdateUI(HeartParent, hearts, (int)healthStats.maxHp.Value, heartSlot, (int)healthStats.curHp.Value);
            UpdateUI(InventoryParent, inventorySlots, inventory.Items.Count, inventorySlot, -1);
        }
    }
    void UpdateUI(Transform parent, List<Image> slots, int targetCount, GameObject slotPrefab, int fillAmount)
    {
        if (slots.Count != targetCount)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
            slots.Clear();

            for (int i = 0; i < targetCount; i++)
            {
                GameObject slot = Instantiate(slotPrefab, parent);
                slots.Add(slot.GetComponent<Image>());
            }
        }

        if (fillAmount >= 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].fillAmount = (fillAmount > i) ? 1 : 0;
            }
        }
    }
    public void SetPlayer(Actor player)
    {
        actor = player;
    }
}
