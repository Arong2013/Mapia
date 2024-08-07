using System;
using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class Sasori : Actor
{
    public Action<Collision2D> OnCollisionEnter2DEvent;
    public Action<Collider2D> OnTriggerExit2DEvents;
    bool isMoveable = true;
    private Puppet puppet;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        SetSkill(); // 스킬 설정
    }

    public override void Move()
    {
        if (isMoveable)
        {
            RB.velocity = movement * GetStatComponent<BaseStats>().speed.Value;
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }

    public override void SetSkill()
    {
        // 스킬 메서드 정의
        System.Action puppetSkillMethod = () =>
        {
            PuppetCreation();
        };
   RegisterSkill(new Skill(skill.icon, skill.cooldown, puppetSkillMethod));
    }

    public void PuppetCreation()
    {
        isMoveable = false;
        PhotonNetwork.Instantiate("Puppet", transform.position, Quaternion.identity);
    }

    public void DestroyPuppet()
    {
        var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
        CM.Follow = transform;
        CM.LookAt = transform;
        isMoveable = true;
        skill.cunCoolTime = skill.cooldown;
    }
}
