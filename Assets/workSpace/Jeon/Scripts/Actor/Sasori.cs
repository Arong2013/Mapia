using System;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Cinemachine;

public class Sasori : Actor
{
    public Action<Collision2D> OnCollisionEnter2DEvent;
    public Action<Collider2D> OnTriggerExit2DEvents;
    bool isMoveable = true;

    Puppet puppet;
    public override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        UiUtils.GetUI<UtilsBtn>().SetButtonAction(() =>
            {
                PuppetCreation();
            });
    }
    public override void Move()
    {
        if (isMoveable)
        {
            RB.velocity = movement;
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }

    }
    public void PuppetCreation()
    {
        isMoveable = false;
        GameObject gameObject = PhotonNetwork.Instantiate("Puppet", transform.position, Quaternion.identity);
    }

    public void DestoryPuppet()
    {
        var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
        CM.Follow = transform;
        CM.LookAt = transform;
        isMoveable = true;
    }
}