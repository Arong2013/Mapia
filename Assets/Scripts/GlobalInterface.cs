using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
public interface IPickupable
{
    public void Pickup();
}
public interface IStatComponent
{
    void RemoveModifiersFromSource(object source);
}

public interface IStatusable
{
    public HashSet<IStatComponent> StatComponents { get; }

    public T GetStatComponent<T>() where T : IStatComponent;

}
public interface ICallNodeDataHandler<T>
where T : Node
{
    void SetData(Node data);
}
public interface IAnimatable
{
    void SetAnimator(AnimatorOverrideController animatorController, bool isSet = false);
    bool CanPlayAnimation(string _animeName);
    void PlayAnimation(string _animeName, object key = null);
}

public interface IPlayerable
{
    void SetPlayer(Actor player);
}