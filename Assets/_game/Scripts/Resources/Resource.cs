using System;
using UnityEngine;

public abstract class Resource : MonoBehaviour, IDeathEvent, IResourceble
{
    public event Action<IDeathEvent> Dead;

    public abstract ResourceType ResourceType { get; }

    public void Die()
    {
        Dead?.Invoke(this);
    }
}