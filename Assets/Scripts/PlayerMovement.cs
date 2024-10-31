using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float crouchSpeedDivisor = 3f;
    [Header("Jump Settings")]
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;
    public float crouchJumpBoost = 2.5f;
    private float jumpTime;
    [Header("Motions")]
    public bool isCrouching = false;
    public bool onGround = false;
    public bool isJumping = false;
    public bool isHeadBlocked = false;
    [Header("Environment")]
    public float footOffset = 0.4f;
    public float headClearance = 0.5f;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;
    [Header("Input Settings")]
    private bool jumpPressed;
    private bool jumpHeld;
    private bool crouchHeld;

    private float xVelocity;
    private Vector2 colliderStandSize;
    private Vector2 colliderStandOffset;
    private Vector2 colliderCrouchSize;
    private Vector2 colliderCrouchOffset;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderCrouchSize = new Vector2(coll.size.x, coll.size.y / 2f);
        colliderCrouchOffset = new Vector2(coll.offset.x, coll.offset.y / 2f);
    }

    void Update()
    {
        crouchHeld = Input.GetButton("Crouch");
        jumpHeld = Input.GetButton("Jump");
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        MidAirMovement();
        GroundMovement();
        ResetMotions();
    }

    private void ResetMotions()
    {
        jumpPressed = false;
        jumpHeld = false;
        crouchHeld = false;
    }


    private void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0), Vector2.down, groundDistance, groundLayer);
        isHeadBlocked = Raycast(new Vector2(0, coll.size.y), Vector2.up, headClearance, groundLayer);
        onGround = leftCheck || rightCheck;
    }

    private void GroundMovement()
    {
        if (crouchHeld && !isCrouching && onGround)
            ToggleCrouch(true);
        else if (((!crouchHeld && !isHeadBlocked) || !onGround) && isCrouching)
            ToggleCrouch(false);

        xVelocity = Input.GetAxis("Horizontal");

        if (isCrouching)
            xVelocity /= crouchSpeedDivisor;

        rb.velocity = new Vector2(xVelocity * moveSpeed, rb.velocity.y);
        FlipDirection();
    }

    private void FlipDirection()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector2(-1, 1);
        else if (xVelocity > 0)
            transform.localScale = new Vector2(1, 1);
    }

    void ToggleCrouch(bool crouching)
    {
        isCrouching = crouching;

        coll.size = isCrouching ? colliderCrouchSize : colliderStandSize;
        coll.offset = isCrouching ? colliderCrouchOffset : colliderStandOffset;
    }

    private void MidAirMovement()
    {
        if (jumpPressed && onGround && !isJumping)
        {
            isJumping = true;
            jumpTime = Time.time + jumpHoldDuration;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (isCrouching)
            {
                ToggleCrouch(false);
                rb.AddForce(Vector2.up * crouchJumpBoost, ForceMode2D.Impulse);
            }
        }
        else if (isJumping)
        {
            if (jumpHeld)
                rb.AddForce(Vector2.up * jumpHoldForce, ForceMode2D.Impulse);
            if (jumpTime < Time.time)
                isJumping = false;
        }
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(position + offset, rayDirection, length, layer);
        Color color = hit ? Color.green : Color.red;
        Debug.DrawRay(position + offset, rayDirection * length, color);
        return hit;
    }
}
