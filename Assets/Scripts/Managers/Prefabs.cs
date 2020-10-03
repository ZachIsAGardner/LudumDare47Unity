using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prefabs: SingleInstance<Prefabs>
{
    public List<GameObject> Items;

    public static GameObject Get(string name)
    {
        return Instance.Items.First(i => i.name == name);
    }
}