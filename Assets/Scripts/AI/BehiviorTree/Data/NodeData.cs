using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class NodeData : ScriptableObject
{
    public abstract Node GetNode(params object[] objects);
}

[System.Serializable]
public abstract class Node
{
    protected NodeState state;

    public Node parent;
    protected List<Node> children = new List<Node>();

    Dictionary<Type, object> datadic = new Dictionary<Type, object>();
    public NodeData Data { get; private set; }
    public Node(NodeData data) => Data = data;

    public Node()
    {
        parent = null;
    }
    public Node(List<Node> children)
    {
        foreach (Node child in children)
        {
            _Attach(child);
        }

    }
    public void _Attach(Node node)
    {
        node.parent = this;
        children.Add(node);
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

     public T GetDeta<T>()
    {
        object value = null;
        if (datadic.TryGetValue(typeof(T), out value))
            return (T)value;
        Node node = parent;
        while (node != null)
        {
            value = node.GetDeta<T>();
            if (value != null)
                return (T)value;
            node = node.parent;
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


    public Node GetNextSibling(Node node)
    {
        if (parent != null && parent.GetType() != typeof(Selector))
        {
            return parent.GetNextSibling(node);
        }
        else
        {
            int currentIndex = children.IndexOf(node);
            if (currentIndex >= 0 && currentIndex < children.Count - 1)
            {
                return children[currentIndex + 1];
            }
            return null;
        }

    }
}