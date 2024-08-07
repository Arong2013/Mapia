using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Chaser : Actor
{
    public Action<Collision2D> OnCollisionEnter2DEvent;
    public Action<Collider2D> OnTriggerExit2DEvents;
    [SerializeField] private int MaxKillCount;
    [SerializeField] private int requiredKillCountForAssassination = 5; // 암살을 위한 최소 킬 카운트

    private Dictionary<string, int> killCount = new Dictionary<string, int>();
    public GameObject DropObj;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        OnTriggerExit2DEvents += (Collider2D other) =>
        {
            UiUtils.GetUI<UtilsBtn>().ResetButton();
        };
    }
    public override void Move()
    {
        RB.velocity = movement * GetStatComponent<BaseStats>().speed.Value;
        PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
    }
    public override void SetSkill()
    {
        System.Action skillMethod = () =>
        {
            TryAssassinate();
        };
        // 스킬 등록
        RegisterSkill(new Skill(skill.icon, skill.cooldown, skillMethod));
    }
    private void TryAssassinate()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent<Actor>(out Actor targetActor) && targetActor != this)
            {
                float distance = Vector3.Distance(transform.position, targetActor.transform.position);
                if (distance <= 3.0f && CanAssassinate(targetActor.ID))
                {
                    Debug.Log("Assassination skill used on target: " + targetActor.ID);
                    skill.cunCoolTime = skill.cooldown;
                }
                else
                {
                    Debug.Log("Cannot assassinate. Either not enough kills or target is too far.");
                }
            }
        }
    }

    public void AddKillCount(string actorID)
    {
        if (killCount.TryGetValue(actorID, out int count))
        {
            if (count < MaxKillCount)
            {
                count++;
                killCount[actorID] = count; // 업데이트된 값을 다시 저장
            }
        }
        else
        {
            killCount[actorID] = 1; // 처음 추가할 때는 1로 시작
        }
    }

    private bool CanAssassinate(string targetActorID)
    {
        return killCount.TryGetValue(targetActorID, out int count) && count >= requiredKillCountForAssassination;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (PV.IsMine)
        {
            OnCollisionEnter2DEvent?.Invoke(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (PV.IsMine)
        {
            OnTriggerExit2DEvents?.Invoke(other);
        }
    }
}
