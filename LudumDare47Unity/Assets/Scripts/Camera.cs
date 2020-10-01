using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{      
    public Player Player;
    public float Speed = 0.5f;

    void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    void LateUpdate()
    {
        if (Player)
        {
            float interpolation = Speed * Time.deltaTime;

            transform.position = new Vector3(
                Player.transform.position.x,
                Player.transform.position.y,
                transform.position.z
            );
        }
    }
}
