using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, ILiverExtra
{
    public float MoveSpeed = 0.25f;

    public void HealthDepleted(HitBox other)
    {
        
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey("d")) {
            transform.position = new Vector3(transform.position.x + (MoveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (Input.GetKey("a")) {
            transform.position = new Vector3(transform.position.x - (MoveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (Input.GetKey("w")) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (MoveSpeed * Time.deltaTime), transform.position.z);
        }

        if (Input.GetKey("s")) {
            transform.position = new Vector3(transform.position.x, transform.position.y - (MoveSpeed * Time.deltaTime), transform.position.z);
        }

        if (Input.GetKeyDown("p"))
        {
            Sound.Play("jump");
        }

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
}
