using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3D : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Offset = new Vector3(0,10,-10);
    public float Acc = 0.1f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Target = FindObjectOfType<Player>()?.gameObject;
    }

    void FixedUpdate()
    {
        Target = FindObjectOfType<Player>()?.gameObject;
        if (Target == null) return;

        var destination = new Vector3(
           Target.transform.position.x + Offset.x, 
           Target.transform.position.y + Offset.y, 
           Target.transform.position.z + Offset.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, Acc);
    }
}
