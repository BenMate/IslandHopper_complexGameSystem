using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof((CharacterController, Animator)))]

public class PlayerMovement : MonoBehaviour
{
    //public float sprintSpeed = 20.0f;
    public float rotationSpeed = 2.0f;
    public float moveSpeed = 5.0f;

    public float pushPower = 2.0f;
    public float jumpPower = 1.0f;

    public float animationSmoothTime = 0.1f;

    public Transform cam;

    bool isGrounded;
    bool isJumping;
    bool attacking;

    Vector3 movement;

    CharacterController cc;
    Animator animator;

    Vector3 velocity = new Vector3();
    Vector3 hitDirection;
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
        UserInput();

        //check if grounded
        isGrounded = cc.isGrounded;

        // check if we've hit ground from falling. If so, remove our velocity 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            isJumping = false;
        }

        //apply gravity
        velocity += Physics.gravity * Time.deltaTime;

        //if we are not grounded, dont scan below the player
        if (!isGrounded)
            hitDirection = Vector3.zero;


        //if we are not grounded, dont scan below the player
        movement = moveSpeed * Time.deltaTime * (moveInput.x * transform.right + moveInput.y * transform.forward);



        //grounded and moving update values
        if (isGrounded || currentAnimationBlendVec.x != 0 || currentAnimationBlendVec.y != 0)
        {
            velocity.x = movement.x;
            velocity.z = movement.z;
        }

        // slide objects off surfaces they're hanging on to 
        if (currentAnimationBlendVec.x == 0 && currentAnimationBlendVec.y == 0)
        {
            Vector3 horizontalHitDirection = hitDirection;
            horizontalHitDirection.y = 0;
            float displacement = horizontalHitDirection.magnitude;
            if (displacement > 0)
                velocity -= 0.2f * horizontalHitDirection / displacement;
        }

        movement += velocity * Time.deltaTime;
        cc.Move(movement);
    }

    void UserInput()
    {
        //character controller info
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        //animator info
        currentAnimationBlendVec = Vector2.SmoothDamp(currentAnimationBlendVec, moveInput, ref AnimationVel, animationSmoothTime);

        animator.SetFloat("MoveX", currentAnimationBlendVec.x);
        animator.SetFloat("MoveZ", currentAnimationBlendVec.y);
        animator.SetBool("Jump", isJumping);
        animator.SetBool("Attack", attacking);

        //if player tries to jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2.0f * Physics.gravity.y);
            isJumping = true;
        }

        //rotate player
        if (Input.GetMouseButton(1) || !isGrounded)
            transform.rotation = transform.rotation;
        else
            transform.rotation = Quaternion.LookRotation(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z));

        //attacking
        attacking = Input.GetMouseButtonDown(0);
    }

    //hides the curser when focised
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
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
