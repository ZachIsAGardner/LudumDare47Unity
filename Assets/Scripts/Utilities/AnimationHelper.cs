using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void PlaySound(string name)
    {
        Sound.Play(name, true, 0.5f);
    }
}
