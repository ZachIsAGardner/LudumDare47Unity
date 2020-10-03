using UnityEngine;

public class SampleScene : MonoBehaviour 
{
    public Trigger Trigger;
    private bool triggered;

    async void Start()
    {
        Trigger.TriggerEnter += Triggered;
        Song.Play("LD47_ow");
    }

    private async void Triggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player")) 
        {
            DisplayText();
        }
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