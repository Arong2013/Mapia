using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Linq;
using Cinemachine;
using System.IO;
using UnityEditor.Animations;


public abstract class Actor : MonoBehaviourPunCallbacks, IPunObservable, IAnimatable
{
    [HideInInspector] public Rigidbody2D RB;
    [HideInInspector] public Animator AN;
    [HideInInspector] public SpriteRenderer SR;
    [HideInInspector] public PhotonView PV;
    [HideInInspector] public float moveHorizontal;
    [HideInInspector] public float moveVertical;

    [SerializeField] protected AnimatorController orianimatorController;

    Dictionary<Type, Node> ActNodeDic = new Dictionary<Type, Node>();
    Vector3 curPos;

    protected HashSet<IStatComponent> statComponents = new HashSet<IStatComponent>();
    public NodeState nodeState = NodeState.FAILURE;

    public string ID => PhotonNetwork.LocalPlayer.ActorNumber.ToString();

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
    }
    void Update()
    {
        if (PV.IsMine)
        {
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


    protected Node GetNode<T>() where T : Node
    {
        var type = typeof(T);
        if (!ActNodeDic.TryGetValue(type, out var node))
        {
            Debug.Log(type + "이 없습니다");
            return null;
        }
        return ActNodeDic[type];
    }

    public void AddNode<T>(Node node, bool IsLower) where T : Node
    {
        var type = typeof(T);
        var parentNode = GetNode<T>();

        if (parentNode == null)
        {
            var sequenceNode = new Sequence(this);
            ActNodeDic.Add(type, sequenceNode);
        }
        ActNodeDic[type].Attach(node, IsLower);
    }
    public NodeState CallAct<T>(T _node) where T : Node
    {
        if (nodeState == NodeState.RUNNING)
            return NodeState.RUNNING;
        var cunSequence = GetNode<T>();

        if (_node != null && _node is ICallNodeDataHandler<T> IcallData)
        {
            IcallData.SetData(cunSequence);
        }
        return cunSequence.Evaluate();
    }

    public void RemoveNode<T>() where T : Node
    {
        var type = typeof(T);
        var parentNode = GetNode<T>();
        if (parentNode == null)
        {
            Debug.LogError(type + "그런 노드 없습니다");
        }
        ActNodeDic.Remove(type);
    }
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
