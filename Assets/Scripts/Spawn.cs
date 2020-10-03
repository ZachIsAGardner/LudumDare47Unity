using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int SpawnID = 0;

    [ExecuteInEditMode]
    void Update()
    {
        name = $"Spawn {SpawnID}";
    }
}
