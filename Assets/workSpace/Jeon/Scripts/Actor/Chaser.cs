using System;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Chaser : Actor
{
    public Action<Collision2D> OnCollisionEnter2DEvent;

    public Action<Collider2D> OnTriggerExit2DEvents;
    [SerializeField] int MaxKillCount;
    Dictionary<string, int> killCount = new Dictionary<string, int>();
    public GameObject DropObj;
    public override void Awake()
    {
        base.Awake();
        statComponents.Add(new MovementStats());
        AddNode<MovementNode>(new MovementNode(this), true);
        AddNode<MovementNode>(new ChaserDrop(this), true);
        AddNode<AttackNode>(new AttackNode(), true);
    }
    protected override void Start()
    {
        base.Start();
        OnCollisionEnter2DEvent += AddKillBtn;
        OnTriggerExit2DEvents += (Collider2D other) =>
        {
            UiUtils.GetUI<UtilsBtn>().ResetButton();
        };
    }
    public override void Move()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0).normalized;
        RB.velocity = movement * 4f;
        CallAct<MovementNode>(new MovementNode(movement));
        PV.RPC(nameof(FlipXRPC), RpcTarget.AllBuffered, moveHorizontal);
    }

    public void AddKillCount(string _name)
    {
        if (killCount.TryGetValue(_name, out int a))
        {
            if (a < MaxKillCount)
                a++;
        }
        else
        {
            killCount.Add(_name, 0);
        }
    }

    void AddKillBtn(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Actor>(out Actor actor) && other.gameObject != this.gameObject)
        {
            Debug.Log(actor.ID);
            UiUtils.GetUI<UtilsBtn>().SetButtonAction(() =>
            {
                CallAct<AttackNode>(new AttackNode(actor));
            });
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (PV.IsMine)
        {
            OnCollisionEnter2DEvent?.Invoke(other);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (PV.IsMine)
        {
            OnTriggerExit2DEvents?.Invoke(other);
        }

    }
}



public class ChaserDrop : Node
{
    float cunTime;
    readonly Actor target;
    public ChaserDrop(Actor _target)
    {
        target = _target;
    }
    public override NodeState Evaluate()
    {
        cunTime++;
        if (cunTime > 100)
        {
            int rand = UnityEngine.Random.Range(1, 10);
            if (rand > 7)
            {
                GameManager.Instance.ShowObjectToPlayer(PhotonNetwork.LocalPlayer.ActorNumber.ToString(), target.transform.position);
            }
            cunTime = 0;
        }

        return NodeState.SUCCESS;
    }
}