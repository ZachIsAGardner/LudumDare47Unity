using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public PromptedTrigger HouseTrigger;
    public Trigger ExitTrigger;
    private bool triggered = false;
    public Animator HouseDoorAnimator;

    public GameObject GiftButterKnife;
    public GameObject GrassContainer;

    public GameObject GiftAxe;

    void Start()
    {
        GiftButterKnife = GameObject.Find("GiftButterKnife");
        GrassContainer = GameObject.Find("GrassContainer");
        Song.Play("Overworld");

        ExitTrigger.TriggerEnter += ExitTriggered;
        HouseTrigger.Accepted += HouseAccepted;
        HouseTrigger.Entered += HouseEntered;
        HouseTrigger.Exited += HouseExited;

        if (Story.Flags.Contains("ButterKnifeGet") || Game.CurrentDay != 0) GiftButterKnife.SetActive(false);
        if (Story.Flags.Contains("Grass3") || Game.CurrentDay != 0) GrassContainer.SetActive(false);

        if (Game.CurrentDay != 2) GiftAxe.SetActive(false);
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
}
