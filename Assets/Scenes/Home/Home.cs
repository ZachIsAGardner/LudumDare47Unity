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
    public PromptedTrigger TreeTrigger;
    public GameObject HouseContainer;

    private int treeHitCount = 0;

    void Start()
    {
        GiftButterKnife = GameObject.Find("GiftButterKnife");
        GrassContainer = GameObject.Find("GrassContainer");

        GiftAxe = GameObject.Find("GiftAxe");
        TreeTrigger = GameObject.Find("TreeTrigger").GetComponent<PromptedTrigger>();
        HouseContainer = GameObject.Find("HouseContainer");

        Song.Play("Overworld");

        ExitTrigger.TriggerEnter += ExitTriggered;
        HouseTrigger.Accepted += HouseAccepted;
        HouseTrigger.Entered += HouseEntered;
        HouseTrigger.Exited += HouseExited;

        if (Story.Flags.Contains("ButterKnifeGet") || Game.CurrentDay != 0) GiftButterKnife.SetActive(false);
        if (Story.Flags.Contains("Grass3") || Game.CurrentDay != 0) GrassContainer.SetActive(false);

        if (Game.Inventory.Contains("Axe")) GiftAxe.SetActive(false);

        if (Game.CurrentDay != 2)
        {
            GiftAxe.SetActive(false);
        }
        else
        {
            TreeTrigger.Accepted += async (object source, bool hit) =>
            {
                treeHitCount += 1;

                if (treeHitCount == 1)
                {
                    Story.Flags.Add("Tree1");
                    _ = Dialogue.Single(new TextBoxModel(
                        text: "Nice hit! Keep going!",
                        speaker: $"{Story.Narrator}"
                    ));
                }

                if (treeHitCount == 4)
                {
                    Story.Flags.Add("Tree2");
                    _ = Dialogue.Single(new TextBoxModel(
                        text: "Okay I know what you're thinking. \"This tree is going to fall onto my house.\" Well you're wrong. I'm an expert trust me.",
                        speaker: $"{Story.Narrator}"
                    ));
                }

                if (treeHitCount == 9)
                {
                    TreeTrigger.Deactivate();
                    HouseContainer.GetComponent<Animator>().SetInteger("State", 2);

                    Story.Flags.Add("Tree3");
                    Song.ChangePitch(0f, 0.5f);
                    HouseTrigger.gameObject.SetActive(false);
                    await Dialogue.Single(new TextBoxModel(
                        text: "Oh!",
                        speaker: $"{Story.Narrator}"
                    ));

                    await new WaitForSeconds(2.5f);
                    Story.Flags.Add("Tree4");
                    await Dialogue.Single(new TextBoxModel(
                        text: "...",
                        speaker: $"{Story.Narrator}"
                    ));

                    await new WaitForSeconds(2.5f);
                    Story.Flags.Add("Tree5");
                    Song.ChangePitch(1f, 0.125f);
                    var textBox = await Dialogue.Begin(new TextBoxModel(
                        text: "Oh well!",
                        speaker: $"{Story.Narrator}"
                    ));

                    Story.Flags.Add("Tree6");
                    await Dialogue.Next(textBox, new TextBoxModel(
                        text: "It was kind of an eye sore anyways.",
                        speaker: $"{Story.Narrator}"
                    ));

                    Story.Flags.Add("Tree7");
                    await Dialogue.End(textBox, new TextBoxModel(
                        text: "I think that's enough for now anyways. Thanks for being such a good listener :)",
                        speaker: $"{Story.Narrator}"
                    ));

                    await new WaitForSeconds(0.5f);

                    await Game.LoadAsync("End", Prefabs.Get<SceneTransition>("FadeSceneTransition"));
                }
                else if (treeHitCount < 9)
                {
                    HouseContainer.GetComponent<Animator>().SetInteger("State", 1);
                    await new WaitForUpdate();
                    HouseContainer.GetComponent<Animator>().SetInteger("State", -1);
                }
            };
        }
    }

    void Update()
    {
        if (!Story.Flags.Contains("Tree3")) 
        {
            if (Game.Inventory.Contains("Axe"))
            {
                TreeTrigger.gameObject.SetActive(true);
            }
            else
            {
                TreeTrigger.gameObject.SetActive(false);
            }
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
}
