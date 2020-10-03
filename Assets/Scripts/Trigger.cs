using System;
using UnityEngine;

public class TriggerEnterEventArgs
{
    public Collider Other { get; private set; }

    public TriggerEnterEventArgs(Collider other)
    {
        Other = other;
    }
}

public class Trigger : MonoBehaviour
{
    public event EventHandler<TriggerEnterEventArgs> TriggerEnter;

    void OnTriggerEnter(Collider other)
    {
        if (TriggerEnter != null) TriggerEnter(this, new TriggerEnterEventArgs(other));
    }
}