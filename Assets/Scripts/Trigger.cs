using System;
using UnityEngine;

public class TriggerEnterEventArgs
{
    public Collider2D Other { get; private set; }

    public TriggerEnterEventArgs(Collider2D other)
    {
        Other = other;
    }
}

public class Trigger : MonoBehaviour
{
    public event EventHandler<TriggerEnterEventArgs> TriggerEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (TriggerEnter != null) TriggerEnter(this, new TriggerEnterEventArgs(other));
    }
}