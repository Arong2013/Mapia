using Photon.Pun;
using UnityEngine;

public class TrapMaker : Actor
{
    [SerializeField] private GameObject trapPrefab; // 트랩 프리팹
    private bool isMoveable = true;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        SetSkill(); // 스킬 설정
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTrap();
        }
    }
    public override void SetSkill()
    {
        System.Action trapSkillMethod = () =>
        {
            TryPlaceTrap();
        };
        RegisterSkill(new Skill(skill.icon, skill.cooldown, trapSkillMethod));
    }

    private void TryPlaceTrap()
    {
        if (PV.IsMine)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePosition, transform.position) <= 3.0f)
            {
                PlaceTrap(mousePosition);
            }
        }
    }
    private void PlaceTrap(Vector2 position)
    {
        skill.cunCoolTime = skill.cooldown;
        PhotonNetwork.Instantiate(trapPrefab.name, position, Quaternion.identity);
    }

    public override void Move()
    {
        if (isMoveable)
        {
            RB.velocity = movement;
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
        }
    }
}
