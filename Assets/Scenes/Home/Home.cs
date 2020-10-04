using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public Trigger Trigger;
    public PromptedTrigger HouseTrigger;
    public Trigger ExitTrigger;
    private bool triggered = false;
    public Animator HouseDoorAnimator;

    void Start()
    {
        Song.Play("LD47_ow");

        Dialogue.Single(new TextBoxModel(
            text: "Eliminate the boxes"
        ));

        Trigger.TriggerEnter += Triggered;
        ExitTrigger.TriggerEnter += ExitTriggered;
        HouseTrigger.PromptedTriggerAccepted += HouseAccepted;
        HouseTrigger.PromptedTriggerEntered += HouseEntered;
        HouseTrigger.PromptedTriggerExited += HouseExited;
    }

    private async void Triggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            DisplayText();
        }
    }

    private async void ExitTriggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            Game.LoadAsync("Exit", Prefabs.Get<SceneTransition>("FadeSceneTransition"));
        }
    }

    private async void HouseEntered(object source, bool hit) 
    {
        HouseDoorAnimator.SetInteger("State", 1);
    }

    private async void HouseExited(object source, bool hit) 
    {
        HouseDoorAnimator.SetInteger("State", 0);
    }

    private async void HouseAccepted(object source, bool hit)
    {
        Game.LoadAsync("InsideHome", Prefabs.Get<SceneTransition>("FadeSceneTransition"));
    }

    private async void DisplayText()
    {
        if (triggered) return;

        triggered = true;
        
        var textBox = await Dialogue.Begin(new TextBoxModel(
            text: "Hello it is I, the narrator."
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: "I am epic."
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Watch out for my stuff."
        ));
    }
}
