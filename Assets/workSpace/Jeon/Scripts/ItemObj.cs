using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ItemObj : MonoBehaviourPunCallbacks, IPickupable
{
    [SerializeField] private ItemData itemData;
    [HideInInspector] public SpriteRenderer SR;
    [HideInInspector] public PhotonView PV;
    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();

        if (itemData != null)
        {
            SR.sprite = itemData.IconSprite;
        }
        else
        {
            Debug.LogError("ItemData is not assigned.");
        }
    }
    public bool Pickup(Actor actor)
    {
        var isCanAdd = actor.inventory.AddItem(itemData.CreateItem());
        if (isCanAdd)
        {
            PhotonNetwork.Destroy(this.gameObject);
            return true;
        }
        return false;
    }
}
