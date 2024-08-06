using Photon.Pun;
using UnityEngine;

public class DeathNote : Actor
{
    [SerializeField] private Sprite skillIcon; // 스킬 아이콘
    [SerializeField] private float skillCooldown = 10.0f; // 스킬 쿨타임

    private DeathNote_Writing MyNote;
    private bool isMoveable = true;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        MyNote = UiUtils.GetUI<DeathNote_Writing>();
        Debug.Log(GetComponent<DeathNote>().ID);
        Debug.Log(PV.Owner.NickName);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ToggleDeathNote();
        }
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
            ToggleDeathNote();
        };

        // 스킬 등록
        RegisterSkill(new Skill(skillIcon, skillCooldown, skillMethod));
    }

    private void ToggleDeathNote()
    {
        if (MyNote.gameObject.activeSelf == false)
        {
            MyNote.gameObject.SetActive(true);
        }
        else
        {
            MyNote.gameObject.SetActive(false);
        }
    }
}
