using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SelectorData", menuName = "BT/SelectorData", order = 4)]
public class SelectorData : NodeData
{
   [SerializeField] List<NodeData> datas;
    public override Node GetNode(params object[] objects)
    {
        List<Node> nodeList = new List<Node>();
        foreach(NodeData data in datas)
        {
            nodeList.Add(data.GetNode());
        }        
        return new Selector(nodeList);
    }
}
    
public class Selector : Node
    {
        public Selector(): base() { }
        public Selector(List<Node> childern) : base(childern) { }   
        public override NodeState Evaluate()
        {
            foreach(Node node in children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;   
        }
    }