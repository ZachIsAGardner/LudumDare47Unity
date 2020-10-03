﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : SingleInstance<Game>
{
    public static bool IsPaused = false;

    public static GameObject Dynamic
    {
        get
        {
            if (dynamic == null)
            {
                dynamic = GameObject.Find("_Dynamic");
                if (dynamic == null) dynamic = new GameObject("_Dynamic");
            }
            return dynamic;
        }
    }
    private static GameObject dynamic;

    private static GameObject dynamicCanvasInstance;

    // ---


    async void Start()
    {
        Screen.SetResolution(960, 540, false);
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (!Screen.fullScreen)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else
            {
                Screen.SetResolution(960, 540, false);
            }
        }
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

    /// <summary>
    /// Spawns a copy of the provided prefab inside the DynamicCanvasFolder GameObject.
    /// <param name="prefab">The GameObject to create a copy of.</param>
    /// <param name="hierarchy">The sorting layer to place the prerab instance into. Higher numbers have priority of lower ones.</param>
    /// </summary>
    public static GameObject NewCanvasElement(GameObject prefab, int hierarchy = 0)
    {
        List<GameObject> folders = new List<GameObject>();

        int i = 0;
        while (i <= hierarchy)
        {
            string name = $"_DynamicCanvasFolder_{i}";
            GameObject folder = GameObject.Find(name);

            // Create new folder
            if (folder == null)
            {
                if (dynamicCanvasInstance == null)
                {
                    dynamicCanvasInstance = Instantiate(Prefabs.Get("Canvas"));
                    dynamicCanvasInstance.name = "DynamicCanvas";
                }

                folder = new GameObject(name);
                folder.AddComponent<RectTransform>();

                folder.GetComponent<RectTransform>().SetParent(dynamicCanvasInstance.GetComponent<RectTransform>());

                folder.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                folder.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                folder.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                folder.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                folder.GetComponent<RectTransform>().localScale = Vector3.one;
            }

            folders.Add(folder);

            i++;
        }

        return Instantiate(
            original: prefab,
            parent: folders[hierarchy].GetComponent<RectTransform>()
        );
    }
}
