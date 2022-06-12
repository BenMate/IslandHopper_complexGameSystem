using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof((CharacterController, Animator)))]

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10.0f;
    public float pushPower = 2.0f;
    public bool isGrounded = false;

    public float acceleration;
    public Vector3 velocity = new Vector3();
    public Vector3 hitDirection;

    [SerializeField]
    float animationSmoothTime = 0.1f;

    CharacterController cc;
    Animator animator;

    Vector2 moveInput;

    Vector2 currentAnimationBlendVec;
    Vector2 AnimationVel;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        //character controller info
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        //animator info

        currentAnimationBlendVec = Vector2.SmoothDamp(currentAnimationBlendVec, moveInput, ref AnimationVel, animationSmoothTime);

        animator.SetFloat("MoveX", currentAnimationBlendVec.x);
        animator.SetFloat("MoveZ", currentAnimationBlendVec.y);
        animator.SetBool("isIdle", moveInput.x == 0 && moveInput.y == 0);
    }

    void FixedUpdate()
    {
        //move character with wasd
        Vector3 move;
        move = moveSpeed * Time.fixedDeltaTime * (currentAnimationBlendVec.x * transform.right + currentAnimationBlendVec.y * transform.forward);

        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = move.x;
            velocity.z = move.z;
        }

        // check if we've hit ground from falling. If so, remove our velocity 
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        //apply gravity
        velocity += Physics.gravity * Time.fixedDeltaTime;

        if (!isGrounded)
            hitDirection = Vector3.zero;

        // slide objects off surfaces they're hanging on to 
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            Vector3 horizontalHitDirection = hitDirection;
            horizontalHitDirection.y = 0;
            float displacement = horizontalHitDirection.magnitude;
            if (displacement > 0)
                velocity -= 0.2f * horizontalHitDirection / displacement;
        }

        move += velocity * Time.fixedDeltaTime;

        cc.Move(move);
        isGrounded = cc.isGrounded; 
    }

    //send a raycast under the player to slide
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitDirection = hit.point - transform.position;

        //if component has a rigid body attached - using push power you can move the item
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
        //--------------------------------------------------------------------------------
    }

}
