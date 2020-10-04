using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public string GiftType = "ButterKnife";

    private PromptedTrigger promptedTrigger;
    private Animator animator;
    private GameObject plane;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        promptedTrigger = GetComponentInChildren<PromptedTrigger>();
        promptedTrigger.Accepted += TriggerAccepted;
        plane = GetComponentsInChildren<Transform>().ToList().Find(t => t.gameObject.name == "Plane").gameObject;
    }

    private async void TriggerAccepted(object source, bool hit) 
    {
        animator.SetInteger("State", 1);
        promptedTrigger.Deactivate();
    }

    public void GetItem()
    {
        var effect = Game.New(Prefabs.Get("SimpleEffect"));
        effect.transform.position = plane.transform.position;

        Destroy(plane);

        if (GiftType == "ButterKnife")
        {
            Story.ButterKnifeGet();
            Game.Inventory.Add("ButterKnife");
        }
        else if (GiftType == "Axe")
        {
            Story.AxeGet();
            Game.Inventory.Add("Axe");
        }
    }
}
