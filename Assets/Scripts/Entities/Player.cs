using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, ILiverExtra, IAnimatorExtra
{
    public float MoveSpeed = 0.25f;
    public List<Sprite> Sprites;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Mover mover;

    private Vector2 velocity = new Vector2(0, 0);

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        animator.Setup(Sprites);
        mover = GetComponentInChildren<Mover>();
    }

    protected override void Update()
    {
        base.Update();
        
        Move();
    }

    private void Move() 
    {
        velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow)) {
            velocity = new Vector2(velocity.x + MoveSpeed, velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            velocity = new Vector2(velocity.x - MoveSpeed, velocity.y);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            velocity = new Vector2(velocity.x, velocity.y + MoveSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            velocity = new Vector2(velocity.x, velocity.y - MoveSpeed);
        }

        mover.Move(velocity * Time.deltaTime);
    }

    public void HealthDepleted(HitBox other)
    {
        
    }

    public AnimationState CurrentState()
    {
        if (velocity == Vector2.zero) 
        {
            return new AnimationState("Idle", new IntegerRange(0,0));
        }
        else 
        {
            return new AnimationState("Walk", new IntegerRange(1,3));
        }
    }
}
