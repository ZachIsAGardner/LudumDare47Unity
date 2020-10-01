using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : SingleInstance<Game>
{
    public static GameObject Dynamic
    {
        get {
            if (dynamic == null)
            {
                dynamic = GameObject.Find("_Dynamic");
                if (dynamic == null) dynamic = new GameObject("_Dynamic");
            }
            return dynamic;
        }
    }
    private static GameObject dynamic;

    public static void Pause()
    {
        print("pause");
    }

    /// <summary>
    /// Load a scene with the provided name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public static void Load(string sceneName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create a new GameObject in the _Dynamic folder.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject New(string name) 
    {
        GameObject go = new GameObject();
        go.transform.parent = Dynamic.transform;
        return go;
    }
}
