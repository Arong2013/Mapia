using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[System.Serializable]
public abstract class CombatNode : Node
{
    protected Transform transform  => nodeActor.transform;
    protected IAnimatable animatable => nodeActor.GetComponent<IAnimatable>();
}

public class AttackNode : Node,ICallNodeDataHandler<AttackNode>
{
    readonly Actor targetActor;
     public AttackNode( )
    {
        
    }
    public AttackNode(Actor _targetActor)
    {
        targetActor = _targetActor;
    }
    public override NodeState Evaluate()
    {
        var cuntarget = GetData<Actor>();
        GameManager.Instance.DestroyGameobject(cuntarget.ID);
        return NodeState.SUCCESS;
    }
    public void SetData(Node data)
    {
        data.SetData(targetActor);
    }
}
public class OnDamageNode : Node
{
    public OnDamageNode()
    {

    }
    public override NodeState Evaluate()
    {
        return base.Evaluate();
    }
}
