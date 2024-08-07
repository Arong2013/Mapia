using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] Image skillSlot;
    [SerializeField] Image cooldownOverlay;
    [SerializeField] TextMeshProUGUI cooldownText;

    void UpdateUI()
    {
        UpdateHeartUI();
        UpdateInventoryUI();
        UpdateSkillUI(); // 스킬 슬롯 UI 업데이트 추가
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
                Debug.Log(i);
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
    void UpdateSkillUI()
    {
        var skill = actor.skill;
        if (skill != null)
        {
            skillSlot.sprite = skill.icon;
            skillSlot.color = Color.white; // 아이콘이 보이도록 색상을 설정

            if (skill.cunCoolTime > 0)
            {
                cooldownOverlay.gameObject.SetActive(true);
                cooldownOverlay.fillAmount = skill.cunCoolTime / skill.cooldown;
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = Mathf.Ceil(skill.cunCoolTime).ToString();
            }
            else
            {
                cooldownOverlay.gameObject.SetActive(false);
                cooldownText.gameObject.SetActive(false);
            }
        }
        else
        {
            skillSlot.sprite = null;
            skillSlot.color = Color.clear; // 아이콘이 없을 때는 투명하게 설정
            cooldownOverlay.gameObject.SetActive(false);
            cooldownText.gameObject.SetActive(false);
        }
    }
    public void SetPlayer(Actor player)
    {
        actor = player;
        actor.inventory.ItemChangeActions += UpdateInventoryUI;
        UpdateSkillUI();
    }
}
