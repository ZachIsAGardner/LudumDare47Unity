using System;
using UnityEngine;

public class PromptedTrigger : MonoBehaviour
{
    public event EventHandler<bool> PromptedTriggerEntered;
    public event EventHandler<bool> PromptedTriggerAccepted;
    public event EventHandler<bool> PromptedTriggerExited;

    private bool isAsking = false;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            isAsking = true;
            player.InspectPrompt.SetActive(true);
            if (PromptedTriggerEntered != null) PromptedTriggerEntered(this, true);
        }
    }

    void Update()
    {
        if (isAsking && Input.GetKeyDown("z"))
        {
            if (PromptedTriggerAccepted != null) PromptedTriggerAccepted(this, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            isAsking = false;
            player.InspectPrompt.SetActive(false);
            if (PromptedTriggerExited != null) PromptedTriggerExited(this, true);
        }
    }
}