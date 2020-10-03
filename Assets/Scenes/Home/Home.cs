using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public Trigger Trigger;
    public PromptedTrigger HouseTrigger;
    private bool triggered = false;

    void Start()
    {
        Song.Play("LD47_ow");

        Dialogue.Single(new TextBoxModel(
            text: "Eliminate the boxes"
        ));

        Trigger.TriggerEnter += Triggered;
        HouseTrigger.PromptedTriggerAccepted += HouseTriggered;
    }

    private async void Triggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            DisplayText();
        }
    }

    private async void HouseTriggered(object source, bool hit)
    {
        Game.Load("InsideHome", 0);
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
