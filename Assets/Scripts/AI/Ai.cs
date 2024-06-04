using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;
using System;

public class Ai : MonoBehaviourPunCallbacks, IPunObservable
{
    [HideInInspector] public Rigidbody2D RB;
    [HideInInspector] public Animator AN;
    [HideInInspector] public SpriteRenderer SR;
    [HideInInspector] public PhotonView PV;
    public Text NickNameText;
    public Image HealthImage;

    public int itemChoice = 0; //아이템 고를때 사용
    public Inventory inventory;
    bool isGround;
    Vector3 curPos;

    Dictionary<Type, Node> actionNodes = new Dictionary<Type, Node>();

    [HideInInspector] public float moveHorizontal;
    [HideInInspector] public float moveVertical;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        AN = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();
        inventory = GameManager.Instance.gameObject.GetComponent<Inventory>();
        // 닉네임
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            // 2D 카메라
            var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;

            actionNodes.Add(typeof(AIUtils.MoveNode), new Sequence(this));
            GetNode<AIUtils.MoveNode>()._Attach(new AIUtils.MoveNode());
        }
    }

    void Update()
    {
        if (PV.IsMine)
        {
            // 이동
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
            CallActionNode<AIUtils.MoveNode>(movement);
        }
        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (collision.GetComponent<Item_Script>().item != null && PV.IsMine)
            {
                inventory.GetItem(collision.GetComponent<Item_Script>().item);
                //UIManager.Instance.SetNowItem(itemChoice);
            }
            else
            {
                Debug.Log("nullnull");
            }

            Destroy(collision.gameObject);
        }
    }

    [PunRPC]
    public void FlipXRPC(float axis) => SR.flipX = axis == -1;

    public void Hit()
    {
        HealthImage.fillAmount -= 0.1f;
        if (HealthImage.fillAmount <= 0)
        {
            GameObject.Find("Canvas").transform.Find("RespawnPanel").gameObject.SetActive(true);
            PV.RPC(nameof(DestroyRPC), RpcTarget.AllBuffered); // AllBuffered로 해야 제대로 사라져 복제버그가 안 생긴다
        }
    }

    [PunRPC]
    public void DestroyRPC() => Destroy(gameObject);

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(HealthImage.fillAmount);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            HealthImage.fillAmount = (float)stream.ReceiveNext();
        }
    }

    public Node GetNode<T>() where T : Node, new()
    {
        var type = typeof(T);
        if (!actionNodes.TryGetValue(type, out var node))
        {
            node = new T();
            actionNodes.Add(type, new Sequence(this));
            actionNodes[type]._Attach(node);
        }
        return actionNodes[type];
    }

    public NodeState CallActionNode<T>(params object[] objects) where T : Node, new()
    {
        var node = GetNode<T>();
        if (objects.Length > 0)
        {
            foreach (object obj in objects)
            {
                node.SetDeta(obj);
            }
        }
        return node.Evaluate();
    }

    public void AddNode<T>(Node node) where T : Node
    {
        actionNodes[typeof(T)]._Attach(node);
    }
}
