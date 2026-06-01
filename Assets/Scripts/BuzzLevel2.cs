using NUnit.Framework;
using UnityEngine;

public class BuzzLevel2 : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public bool applyGravity = true; // Toggle gravity on/off 
    public float runSpeed = 3f;
    private Vector3 velocity;  // Tracks Carl's downward force (gravity)

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();    // Handles pressing W → run straight
        ApplyGravity();      // Applies gravity every frame
        CheckFallOutOfBounds();  // Checks if Carl fell off the map
    }


    // W = RUN STRAIGHT

    void HandleMovement()
    {
        bool running = Input.GetKey(KeyCode.W);

        animator.SetFloat("Blend", running ? 1f : 0f);

        if (running)
        {
            Vector3 move = transform.forward * runSpeed;
            controller.Move(move * Time.deltaTime);
        }

        // lock rotation so he faces forward only
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }


    // GRAVITY

    void ApplyGravity()

    {
        if (!applyGravity)
        {
            return;
        }
        if (controller.isGrounded)
        {
            velocity.y = -2f; // small downward force to keep grounded
        }
        else
        {
            velocity.y += Physics.gravity.y * 2f * Time.deltaTime; //apply gravity manually(characterController doesn't auto-apply gravity)
        }
        // Apply the vertical gravity movement
        controller.Move(velocity * Time.deltaTime);
    }

    // ------------------------------
    // FALL OUT OF MAP
    // ------------------------------
    void CheckFallOutOfBounds()
    {
        if (transform.position.y < -10f)
        {
            transform.parent = null;  // detach from platform if falling
        }
    }
}
