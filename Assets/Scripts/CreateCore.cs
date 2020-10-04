using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCore : MonoBehaviour
{
    public GameObject Core;

    void Awake()
    {
        if (!GameObject.Find("_Core")) 
        {
            var core = Instantiate(Core);
            core.name = Core.name;
            Instantiate(Prefabs.Get("Camera"));
        }
    }
}
