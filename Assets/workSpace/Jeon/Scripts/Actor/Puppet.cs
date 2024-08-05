using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
public class Puppet : MonoBehaviourPunCallbacks, IPunObservable, IAnimatable
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

    Sasori sasori;
    float sasoriTime = 20f;
    float cunTime;
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

    private void Start()
    {
        Actor[] cunActors = GameObject.FindObjectsOfType<Actor>();
        foreach(var cunActor in cunActors)
        {
            if(cunActor.TryGetComponent<Sasori>(out Sasori component))
            {
                sasori = component;
            }
        }
    }
    void Update()
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

    void Move()
    {
        RB.velocity = movement * 5;
        cunTime += Time.deltaTime;
        if(cunTime > sasoriTime)
        {
            sasori.DestoryPuppet();
            PhotonNetwork.Destroy(PV);
        }
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