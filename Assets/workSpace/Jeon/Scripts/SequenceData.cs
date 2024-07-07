using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SequenceData", menuName = "BT/SequenceData", order = 4)]
public class SequenceData : NodeData
{
     [SerializeField] List<NodeData> datas;
    public override Node GetNode(params object[] objects)
    {
        List<Node> nodeList = new List<Node>();
        foreach(NodeData data in datas)
        {
            nodeList.Add(data.GetNode());
        } 
        return new Sequence(nodeList);
    }
}

    public class Sequence : Node
    {
        public Sequence(Actor actor) : base(){ AddActor(actor);}
        public Sequence(List<Node> childern) : base(childern) { }   
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(Node node in children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        return NodeState.RUNNING;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;   
        }
    }
