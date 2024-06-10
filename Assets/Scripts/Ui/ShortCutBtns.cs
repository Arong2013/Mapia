using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutBtns : MonoBehaviour
{
    [SerializeField] Transform parent;

    [SerializeField] Button InventoryBtn,SearchBtn;

    private void Start() 
    {
      //  var inventoryUI = UiUtils.GetUI<InventoryUI>();
      //  InventoryBtn.onClick.AddListener(() => {inventoryUI.OpenInventory(GameManager.Instance.Player.GetComponent<Inventory>());});

    }
}
