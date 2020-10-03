using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, ILiverExtra
{
    public float MoveSpeed = 2f;
    public float Acc = 0.1f;
    public GameObject Model;
    public float Gravity = 0.25f;
    public float MinJumpStrength = 0.5f;
    public float JumpStrength = 1f;
    public GameObject InspectPrompt;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody rigidbody;
    private CapsuleCollider collider;

    private Vector3 startPosition;

    private Vector3 velocity = new Vector3(0, 0, 0);
    private int dir = 0;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

        startPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        Move();

        if (transform.position.y < -20) 
        {
            transform.position = startPosition;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f);
    }

    private void Move()
    {
        var input = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            input.x = 1;
            dir = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            input.x = -1;
            dir = 1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            input.z = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            input.z = -1;
        }

        input.Normalize();

        rigidbody.velocity = new Vector3(
            rigidbody.velocity.x.MoveOverTime(input.x * MoveSpeed, Acc),
            rigidbody.velocity.y - (Gravity * Time.deltaTime),
            rigidbody.velocity.z.MoveOverTime(input.z * MoveSpeed, Acc)
        );

        Model.transform.eulerAngles = new Vector3(0, Model.transform.eulerAngles.y.MoveOverTime(180 * dir, Acc), 0);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y + JumpStrength, rigidbody.velocity.z);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rigidbody.velocity.y > MinJumpStrength)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, MinJumpStrength, rigidbody.velocity.z);
        }
    }

    private void Animate()
    {
        // Idling
        if (velocity.x == 0 && velocity.y == 0)
        {
            animator.SetInteger("State", 0);
        }
        // Walking
        else
        {
            animator.SetInteger("State", 1);
        }
    }

    public void HealthDepleted(HitBox other)
    {

    }
}
