using System;
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
    private Vector3 curPos;
    private Sasori sasori;
    private float sasoriTime = 20f;
    private float cunTime;

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
        sasori = FindObjectsOfType<Actor>().OfType<Sasori>().FirstOrDefault();
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            HandleInput();
            Move();
        }
        else
        {
            SmoothSyncPosition();
        }

        cunTime += Time.deltaTime;
        if (cunTime > sasoriTime)
        {
            sasori.DestroyPuppet();
            PhotonNetwork.Destroy(PV);
        }
    }

    private void HandleInput()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
    }

    private void Move()
    {
        RB.velocity = movement * 5;
    }

    private void SmoothSyncPosition()
    {
        if ((transform.position - curPos).sqrMagnitude >= 100)
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

    public void SetAnimator(AnimatorOverrideController animatorController, bool isSet = false)
    {
        AN.runtimeAnimatorController = isSet ? animatorController : orianimatorController;
    }

    public NodeState IsAnimationPlaying(string animationName)
    {
        var animatorState = AN.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName(animationName))
            return NodeState.RUNNING;

        return animatorState.IsName("Walk") && AN.GetFloat("Walk") == 0
            ? NodeState.SUCCESS
            : NodeState.FAILURE;
    }

    public void PlayAnimation(string animationName, object key = null)
    {
        switch (animationName)
        {
            case "Hit":
                AN.SetTrigger(animationName);
                break;
            case "Attack" when key is float dps:
                AN.SetFloat("DPS", dps);
                AN.SetTrigger(animationName);
                break;
            case "Walk" when key is float speed:
                AN.SetFloat(animationName, speed);
                break;
        }
    }
}
