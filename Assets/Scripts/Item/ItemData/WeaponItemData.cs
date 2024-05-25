
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(fileName = "Item_Weapon_", menuName = "Inventory System/Item Data/Weaopn", order = 1)]
public class WeaponItemData : EquipmentItemData
{
     public int ATK ;
     public float AttackCoolTime ;
     public float Distance;
     public GameObject WeaponPrefab;

     public AnimatorOverrideController animator;
    public override Item CreateItem()
    {
        return new WeaponItem(this);
    }
}

[System.Serializable]
public class WeaponItem : EquipmentItem
{
    int atk; public int ATK => atk;
    float atkCooltime; public float AtkCoolTime => atkCooltime;

    float dis; public float Dis => dis;
    bool isWeaponed = false; public bool IsWeaponed => isWeaponed;

    AnimatorOverrideController animatorController; public AnimatorOverrideController AnimatorController => animatorController;
    
    public WeaponItem(WeaponItemData data) : base(data)
    {
        atk = data.ATK;
        atkCooltime = data.AttackCoolTime;
        dis = data.Distance;
        animatorController = data.animator;
    }

    public bool Use()
    {
        if(Data is WeaponItemData weapon)
        {
            if(!isWeaponed)
            {
                // GameObject weaponPrefabs =   MonoBehaviour.Instantiate(weapon.WeaponPrefab.gameObject, inventory.Weapon);
                // weaponPrefabs.GetComponent<WeaponController>().Init(this);
               //  isWeaponed = true;               
            }                
            else
            {
              //  MonoBehaviour.Destroy(inventory.Weapon.GetChild(0).gameObject);
              //  isWeaponed = false;
            }          
        }
        return false;
    }

    public void RemoveWeapon()
    {
        if(IsWeaponed)
        {
          //  MonoBehaviour.Destroy(inventory.Weapon.GetChild(0).gameObject);
            isWeaponed = false;
        }
    }
}
