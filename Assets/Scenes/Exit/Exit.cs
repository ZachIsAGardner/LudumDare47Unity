using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Trigger BackTrigger;
    public Trigger Trigger;

    void Start()
    {
        BackTrigger.TriggerEnter += BackTriggered;
        Trigger.TriggerEnter += Triggered;
    }

    private async void BackTriggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            Game.LoadAsync("Home", Prefabs.Get<SceneTransition>("FadeSceneTransition"));
        }
    }

    private async void Triggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            Game.LoadAsync("End", Prefabs.Get<SceneTransition>("FadeSceneTransition"));
        }
    }
}
