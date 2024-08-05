using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Linq;
using Cinemachine;
using System.IO;
using UnityEngine.UI;


public enum Camp
{
    None,
    Good,
    Bad,
    Neutral
}


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

    public virtual void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        AN = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();


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

        if (PV.IsMine)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
            Move();
        }
        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
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

    public bool CanPlayAnimation(string animationName)
    {
        if (IsAnimationPlaying("Hit"))
        {
            return false;
        }
        if (IsAnimationPlaying("Attack"))
        {
            return false;
        }
        if (IsAnimationPlaying("Walk"))
        {
            return true;
        }
        return false;
    }
    private bool IsAnimationPlaying(string animationName)
    {
        return AN.GetCurrentAnimatorStateInfo(0).IsName(animationName);
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
}
