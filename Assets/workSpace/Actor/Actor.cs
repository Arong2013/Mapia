using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using Cinemachine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Skill
{
    [PreviewField(Height = 50), HideLabel]
    public Sprite icon; // 스킬 아이콘

    [MinValue(0), LabelText("Cooldown (s)")]
    public float cooldown; // 스킬 쿨타임

    [HideInInspector]
    public float cunCoolTime;

    [HideInInspector, ShowInInspector, LabelText("Skill Method")]
    public Action method; // 스킬 실행 메소드

    public Skill(Sprite icon, float cooldown, Action method)
    {
        this.icon = icon;
        this.cooldown = cooldown;
        this.method = method;
    }
}
public abstract class Actor : MonoBehaviourPunCallbacks, IPunObservable, IAnimatable
{
    [FoldoutGroup("Components"), HideLabel, PreviewField(Height = 50)]
    public Rigidbody2D RB;

    [FoldoutGroup("Components"), HideLabel, PreviewField(Height = 50)]
    public Animator AN;

    [FoldoutGroup("Components"), HideLabel, PreviewField(Height = 50)]
    public SpriteRenderer SR;

    [FoldoutGroup("Components"), HideLabel, PreviewField(Height = 50)]
    public PhotonView PV;

    [FoldoutGroup("Movement Settings")]
    public float moveHorizontal;

    [FoldoutGroup("Movement Settings")]
    public float moveVertical;

    protected Vector3 movement;

    [FoldoutGroup("Animation Settings"), HideLabel, PreviewField(Height = 50)]
    [SerializeField] protected AnimatorOverrideController orianimatorController;

    private Vector3 curPos;

    protected HashSet<IStatComponent> statComponents = new HashSet<IStatComponent>();

    [FoldoutGroup("Player Settings")]
    public Camp PlayerSide = Camp.None;

    [FoldoutGroup("Player Settings"), LabelText("NickName Text")]
    public Text NickNameText;

    [FoldoutGroup("Player Settings"), LabelText("Player ID")]
    public string ID;

    [FoldoutGroup("Inventory")]
    public Inventory inventory = new Inventory();

    [FoldoutGroup("Skill Settings"), HideLabel]
    [ShowInInspector, InlineProperty, ShowIf("@this.skill != null")]
    public Skill skill; // 스킬 변수 추가

    public virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        AN = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();

        statComponents.Add(new BaseStats());
        if (PV.IsMine)
        {
            var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
            PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
            GameManager.Instance.AddPlayer(this);
        }
    }

    protected virtual void Start()
    {   
        NickNameText.text = PV.Owner.NickName;
        SetSkill();
        GameManager.Instance.AddActor();
    }

    protected virtual void Update()
    {
        if (PV.IsMine)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
            Move();

            // 스킬 사용 입력 처리
            if (Input.GetKeyDown(KeyCode.R) && skill.cunCoolTime <= 0)
            {
                UseSkill();
            }
            if(skill.cunCoolTime >= 0)
            {
                skill.cunCoolTime -= Time.deltaTime;
            }
        }
        else
        {
            SyncPosition();
        }
    }

    private void SyncPosition()
    {
        if ((transform.position - curPos).sqrMagnitude >= 100)
            transform.position = curPos;
        else
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
            stream.SendNext(transform.position);
        else
            curPos = (Vector3)stream.ReceiveNext();
    }

    [PunRPC]
    public void FlipXRPC(float axis) => SR.flipX = axis == -1;

    [PunRPC]
    public void DestroyRPC() => Destroy(gameObject);

    public abstract void Move();
    public abstract void SetSkill();

    public T GetStatComponent<T>() where T : IStatComponent
    {
        return statComponents.OfType<T>().FirstOrDefault();
    }

    public void SetAnimator(AnimatorOverrideController animatorController, bool isSet = false)
    {
        AN.runtimeAnimatorController = isSet ? animatorController : orianimatorController;
    }

    public NodeState IsAnimationPlaying(string animationName)
    {
        var animatorState = AN.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName(animationName))
            return NodeState.RUNNING;

        return animatorState.IsName("Walk") && AN.GetFloat("Walk") == 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    public void PlayAnimation(string _animeName, object key = null)
    {
        switch (_animeName)
        {
            case "Hit":
                AN.SetTrigger(_animeName);
                break;
            case "Attack" when key is float dps:
                AN.SetFloat("DPS", dps);
                AN.SetTrigger(_animeName);
                break;
            case "Walk" when key is float speed:
                AN.SetFloat(_animeName, speed);
                break;
        }
    }

    public void RegisterSkill(Skill skill)
    {
        this.skill = skill;
    }

    private void UseSkill()
    {
        skill?.method?.Invoke();
    }
}
