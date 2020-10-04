using System;
using UnityEngine;

public class PromptedTrigger : MonoBehaviour
{
    public event EventHandler<bool> Entered;
    public event EventHandler<bool> Accepted;
    public event EventHandler<bool> Exited;

    private bool isAsking = false;

    public void Deactivate()
    {
        var player = FindObjectOfType<Player>();

        if (player)
        {
            isAsking = false;
            player.InspectPrompt.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void Disable()
    {
        var player = FindObjectOfType<Player>();

        if (player)
        {
            isAsking = false;
            player.InspectPrompt.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            isAsking = true;
            player.InspectPrompt.SetActive(true);
            if (Entered != null) Entered(this, true);
        }
    }

    void Update()
    {
        if (isAsking && Input.GetKeyDown("z"))
        {
            if (Accepted != null) Accepted(this, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            isAsking = false;
            player.InspectPrompt.SetActive(false);
            if (Exited != null) Exited(this, true);
        }
    }
}