using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Character
{
    public float MoveSpeed = 20f;

    protected override void Update()
    {
        transform.position = new Vector3(transform.position.x + (MoveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
