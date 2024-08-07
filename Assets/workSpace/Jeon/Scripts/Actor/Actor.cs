using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Linq;
using Cinemachine;
using System.IO;
using UnityEngine.UI;


public abstract class Actor : MonoBehaviourPunCallbacks, IPunObservable, IAnimatable
{
    [HideInInspector] public Rigidbody2D RB;
    [HideInInspector] public Animator AN;
    [HideInInspector] public SpriteRenderer SR;
    [HideInInspector] public PhotonView PV;
    [HideInInspector] public float moveHorizontal;
    [HideInInspector] public float moveVertical;
    protected Vector3 movement;
    [SerializeField] protected AnimatorOverrideController orianimatorController;
    Vector3 curPos;

    protected HashSet<IStatComponent> statComponents = new HashSet<IStatComponent>();
    public Camp PlayerSide = Camp.None;
    public Text NickNameText;
    public string ID;

    public Inventory inventory = new Inventory();

    public bool DoQuest = false;


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
        }
    }
    protected virtual void Start()
    {
        GameManager.Instance.AddActor();
        NickNameText.text = PV.Owner.NickName;
    }
    protected virtual void Update()
    {
        if (PV.IsMine && !DoQuest)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
            Move();
        }
        else if(PV.IsMine && DoQuest)
        {
            movement = new Vector3(0, 0, 0).normalized;
            Move();
        }
        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) 
            transform.position = curPos;
        else 
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
    [PunRPC]
    public void FlipXRPC(float axis) => SR.flipX = axis == -1;
    [PunRPC]
    public void DestroyRPC() => Destroy(gameObject);
    public abstract void Move();
    public T GetStatComponent<T>() where T : IStatComponent
    {
        return (T)statComponents.FirstOrDefault(component => component is T);
    }

    public void SetAnimator(AnimatorOverrideController animatorController, bool isSet = false)
    {
        if (isSet)
            AN.runtimeAnimatorController = animatorController;
        else
            AN.runtimeAnimatorController = orianimatorController;
    }
    public NodeState IsAnimationPlaying(string animationName)
    {
        if (AN.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            return NodeState.RUNNING;
        else if (AN.GetCurrentAnimatorStateInfo(0).IsName("Walk") && AN.GetFloat("Walk") == 0)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
    public void PlayAnimation(string _animeName, object key = null)
    {
        if (_animeName == "Hit")
        {
            AN.SetTrigger(_animeName);
        }
        if (_animeName == "Attack")
        {
            if (key is float _Dps)
            {
                AN.SetFloat("DPS", _Dps);
                AN.SetTrigger(_animeName);
            }
        }
        if (_animeName == "Walk")
        {
            if (key is float _speed)
            {
                AN.SetFloat(_animeName, _speed);
            }

        }
    }

    public void DoingMission()
    {
        DoQuest = true;
    }

    public void FinishMission()
    {
        Debug.Log("작동되나요");
        DoQuest = false;
    }



}
