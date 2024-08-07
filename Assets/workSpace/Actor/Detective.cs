using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Detective : Actor, IPointerClickHandler
{
    private bool isMoveable = true;
    public Text ActorText;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Move()
    {
        if (isMoveable)
        {
            RB.velocity = movement;
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }

    public override void SetSkill()
    {
        // 스킬 메서드 정의
        System.Action skillMethod = () =>
        {
            OnSkillUse();
        };

        // 스킬 등록
        RegisterSkill(new Skill(skill.icon, skill.cooldown, skillMethod));
    }

    private void OnSkillUse()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            IPointerClickHandler clickHandler = result.gameObject.GetComponent<IPointerClickHandler>();
            if (clickHandler != null && result.gameObject != this.gameObject)
            {
                clickHandler.OnPointerClick(pointerEventData);
                skill.cunCoolTime = skill.cooldown;
                break;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PV.IsMine)
        {
            // ActorText.text = "Actor_What"; // 여기에 실제 정보를 표시
            // PV.Owner.NickName 등을 사용할 수 있습니다.
        }
    }
}
