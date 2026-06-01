using UnityEngine;

public class BuzzBehavior : MonoBehaviour
{
    public trafficlights lightController;
    public CharacterController controller;
    public Animator animator;
    public UI gameUI;
    public float runSpeed = 3f;
    private bool isDead = false;
    private bool hasWon = false; // *** ADDED THIS LINE ***
    private float yellowTimer = 0f;
    public float reactionTime = 0.8f;
    private bool isYellowActive = false;
    private Vector3 velocity;
    public float gravity = -9.81f;

    void Update()
    {
        // Apply gravity every frame, even when dead
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // *** ADDED THIS CHECK - Stop if dead or won ***
        if (isDead || hasWon)
        {
            // Keep playing dance animation if won
            if (hasWon)
            {
                animator.SetBool("Dance", true);
                animator.SetFloat("Blend", 0f);
            }
            return; // EXIT - don't check for violations anymore
        }

        // Rest of your original code - UNCHANGED
        string state = lightController.GetState();
        if (state == "Green")
        {
            isYellowActive = false;
            GreenState();
        }
        else if (state == "Red")
        {
            isYellowActive = false;
            RedState();
        }
        else if (state == "Yellow")
        {
            YellowState();
        }
    }

    void GreenState()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Dance", true);
            animator.SetFloat("Blend", 0f);
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 move = transform.forward * runSpeed * Time.deltaTime;
            controller.Move(move);
            animator.SetFloat("Blend", 1f);
        }
        else
        {
            animator.SetFloat("Blend", 0f);
        }
        animator.SetBool("Dance", false);
    }

    void RedState()
    {
        if (Input.anyKey)
        {
            TriggerDeath();
            return;
        }
        animator.SetFloat("Blend", 0f);
        animator.SetBool("Dance", false);
    }

    void YellowState()
    {
        if (!isYellowActive)
        {
            isYellowActive = true;
            yellowTimer = 0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            TriggerDeath();
            return;
        }
        yellowTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Dance", true);
            animator.SetFloat("Blend", 0f);
            return;
        }
        if (yellowTimer > reactionTime)
        {
            TriggerDeath();
        }
    }

    void TriggerDeath()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Death");
        animator.SetBool("Dance", false);
        animator.SetFloat("Blend", 0f);
        animator.speed = 1f;
        Debug.Log("GAME OVER!");
        if (gameUI != null)
        {
            Debug.Log("Calling ShowDeath()");
            gameUI.ShowDeath();
        }
        else
        {
            Debug.LogError("gameUI reference is MISSING on CarlBehavior!");
        }
    }

    // *** ADDED THIS METHOD - For win condition ***
    void OnTriggerEnter(Collider other)
    {
        // Only trigger win if this is the player reaching finish
        if (other.CompareTag("Finish") && !hasWon && !isDead)
        {
            hasWon = true;
            animator.SetBool("Dance", true);
            animator.SetFloat("Blend", 0f);

            if (gameUI != null)
            {
                gameUI.ShowWin();
            }

            Debug.Log("Player Won!");
        }
    }
}