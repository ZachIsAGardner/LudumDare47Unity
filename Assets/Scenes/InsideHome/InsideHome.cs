using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHome : MonoBehaviour
{
    public Trigger ExitTrigger;

    void Start()
    {
        ExitTrigger.TriggerEnter += Triggered;
        Story.Greeting();
    }

    private async void Triggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            Game.LoadAsync("Home", Prefabs.Get<SceneTransition>("FadeSceneTransition"), 0);
        }
    }
}
