using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InsideHome : MonoBehaviour
{
    public Trigger ExitTrigger;
    public PromptedTrigger BedTrigger;
    private GameObject room;

    void Awake()
    {
        room = GameObject.Find("Room");
        SceneManager.sceneLoaded += BindEvents;

        SceneManager.sceneUnloaded += (Scene scene) =>
        {
            SceneManager.sceneLoaded -= BindEvents;
        };
    }

    void Start()
    {
        Song.Play("Overworld");
        ExitTrigger.TriggerEnter += Triggered;
        BedTrigger.Accepted += async (object source, bool hit) =>
        {
            BedTrigger.Disable();
            bool success = false;

            if (Game.CurrentDay == 0 && Story.Flags.Contains("Grass3"))
            {
                Game.CurrentDay += 1;
                success = true;
                await Game.LoadAsync("InsideHome", Prefabs.Get<SceneTransition>("FadeSceneTransition"), 1);
                return;
            }

            if (Game.CurrentDay == 1 && Story.Flags.Contains("Excercise7"))
            {
                Game.CurrentDay += 1;
                success = true;
                await Game.LoadAsync("InsideHome", Prefabs.Get<SceneTransition>("FadeSceneTransition"), 1);
                return;
            }

            if (!success)
            {
                await Dialogue.Single(new TextBoxModel(
                    text: "(Some sort of abstract goal calls to you elsewhere)",
                    speaker: "Your Inner Consciousness"
                ));
            }
        };
    }

    async void Update()
    {
        if (Game.CurrentDay == 1)
        {
            ExitTrigger.gameObject.SetActive(false);
        }

        // Excercise Day
        if (Game.CurrentDay == 1 && Game.Player != null && Story.Flags.Contains("DayExcerciseEnd"))
        {

            if (Game.Player.JumpCount > 0 && !Story.Flags.Contains("Excercise1"))
            {
                Story.Flags.Add("Excercise1");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "Yeah that's it Keep doing 'em!",
                    speaker: $"{Story.Narrator}"
                ));
            }
            if (Game.Player.JumpCount > 4 && !Story.Flags.Contains("Excercise2"))
            {
                Story.Flags.Add("Excercise2");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "Wow Amazing. 5 whole jumping jacks. I'm sweating just looking at you.",
                    speaker: $"{Story.Narrator}"
                ));
            }
            if (Game.Player.JumpCount > 9 && !Story.Flags.Contains("Excercise3"))
            {
                Story.Flags.Add("Excercise3");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "Yeah you're crazy you're shaking the house! Keep it up!",
                    speaker: $"{Story.Narrator}"
                ));
                room.GetComponent<Animator>().SetInteger("State", 1);
                room.GetComponent<Animator>().speed = 0.25f;
                Song.ChangePitch(1.2f, 0.25f);
                Game.Player.Gravity *= 1.25f;
            }
            if (Game.Player.JumpCount > 14 && !Story.Flags.Contains("Excercise4"))
            {
                Story.Flags.Add("Excercise4");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "<color=\"blue\">Amazing!</color>",
                    speaker: $"{Story.Narrator}"
                ));
                room.GetComponent<Animator>().speed = 0.5f;
                Song.ChangePitch(1.4f, 0.25f);
                Game.Player.Gravity *= 1.25f;
            }
            if (Game.Player.JumpCount > 19 && !Story.Flags.Contains("Excercise5"))
            {
                Story.Flags.Add("Excercise5");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "<color=\"green\">Spectacular!</color>",
                    speaker: $"{Story.Narrator}"
                ));
                room.GetComponent<Animator>().speed = 0.75f;
                Song.ChangePitch(1.6f, 0.25f);
                Game.Player.Gravity *= 1.25f;
            }
            if (Game.Player.JumpCount > 24 && !Story.Flags.Contains("Excercise6"))
            {
                Story.Flags.Add("Excercise6");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "<color=\"red\">Wooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooow!</color>",
                    speaker: $"{Story.Narrator}"
                ));
                Song.ChangePitch(2f, 0.25f);
                room.GetComponent<Animator>().speed = 0.75f;
                Game.Player.Gravity *= 1.25f;
            }
            if (Game.Player.JumpCount > 34 && !Story.Flags.Contains("Excercise7"))
            {
                Story.Flags.Add("Excercise7");
                _ = Dialogue.Single(new TextBoxModel(
                    text: "I would say that is sufficient. You should go get some rest. I have one final task for you tomorrow :)",
                    speaker: $"{Story.Narrator}"
                ));
                room.GetComponent<Animator>().SetInteger("State", 0);
                Song.ChangePitch(1f, 0.25f);
                Game.Player.Gravity = Game.Player.NormalGravity;
            }
        }
        else
        {
            ExitTrigger.gameObject.SetActive(true);
        }
    }

    private void BindEvents(Scene scene, LoadSceneMode mode)
    {
        if (Game.CurrentDay == 0) Story.Greeting();
        if (Game.CurrentDay == 1) Story.DayExcercise();
        if (Game.CurrentDay == 2) Story.DayTree();
    }

    private async void Triggered(object source, TriggerEnterEventArgs args)
    {
        if (args.Other.CompareTag("Player"))
        {
            Game.LoadAsync("Home", Prefabs.Get<SceneTransition>("FadeSceneTransition"), 0);
        }
    }
}
