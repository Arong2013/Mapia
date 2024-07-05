using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class Chaser : Actor
{
    [SerializeField] int MaxKillCount;
    Dictionary<string, int> killCount = new Dictionary<string, int>();
    public GameObject DropObj;
    public override void Awake()
    {
        base.Awake();
        statComponents.Add(new MovementStats());
        AddNode<MovementNode>(new MovementNode(this), true);
        AddNode<MovementNode>(new ChaserDrop(this), true);
    }
    protected override void Start()
    {
       base.Start();
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
            int rand = Random.Range(1, 10);
            if (rand > 7)
            {
                GameManager.Instance.ShowObjectToPlayer(PhotonNetwork.LocalPlayer.ActorNumber.ToString(),target.transform.position);
            }
            cunTime = 0;
        }

        return NodeState.SUCCESS;
    }
}