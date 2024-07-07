using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public abstract class NodeData : ScriptableObject
{
    public abstract Node GetNode(params object[] objects);
}

[System.Serializable]
public abstract class Node
{
    Actor nodeActors;
    public Actor nodeActor
    {
        get
        {
            if (parent != null)
                return parent.nodeActor;
            else
                return nodeActors;
        }
    }
    protected NodeState state;

    protected Node parent;
    protected List<Node> children = new List<Node>();

    Dictionary<Type, object> datadic = new Dictionary<Type, object>();
    public Node()
    {
        parent = null;
    }
    public Node(List<Node> children)
    {
        foreach (Node child in children)
        {
            Attach(child, true);
        }

    }

    public void AddActor(Actor _actor)
    {
        nodeActors = _actor;
    }
    public void Attach(Node node, bool IsLower)
    {
        node.parent = this;
        if (IsLower)
        {
            children.Add(node);
        }
        else
        {
            children.Insert(0, node);
        }
    }
    public virtual NodeState Evaluate() => NodeState.FAILURE;

    public void SetData(object value)
    {
        if (parent != null)
        {
            parent.SetData(value);
        }
        else
        {
            if (datadic.ContainsKey(value.GetType()))
                datadic[value.GetType()] = value;
            else
            {
                datadic.Add(value.GetType(), value);
            }
        }
    }

    public T GetData<T>()
    {
        if (parent != null)
        {
            return parent.GetData<T>();
        }
        else
        {
            foreach (var kvp in datadic)
            {
                if (kvp.Key == typeof(T) || kvp.Key.IsSubclassOf(typeof(T)))
                {
                    if (kvp.Value is T cunkey)
                    {
                        Debug.Log(cunkey);
                        return cunkey;
                    }
                }
            }
        }
        return default(T);
    }

    public bool ClearData<T>()
    {
        if (datadic.ContainsKey(typeof(T)))
        {
            datadic.Remove(typeof(T));
            return true;
        }
        Node node = parent;
        while (node != null)
        {
            bool cleared = node.ClearData<T>();
            if (cleared)
                return true;
            node = node.parent;
        }
        return false;
    }
    // public Node GetNextSibling(Node node)
    // {
    //     if (parent != null && parent.GetType() != typeof(Selector))
    //     {
    //         return parent.GetNextSibling(node);
    //     }
    //     else
    //     {
    //         int currentIndex = children.IndexOf(node);
    //         if (currentIndex >= 0 && currentIndex < children.Count - 1)
    //         {
    //             return children[currentIndex + 1];
    //         }
    //         return null;
    //     }

    // }
}
