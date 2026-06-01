using UnityEngine;

public class npc : MonoBehaviour
{
    public trafficlights lightController;
    public Animator animator;

    [Range(0, 100)] public float accuracy = 80f;  // % chance NPC behaves correctly
    [Range(0, 100)] public float randomDeathChance = 5f;  // % chance to randomly die each state change
    public float moveSpeed = 2f;

    private bool isDead = false;
    private bool hasWon = false;
    private string lastState = "";

    // Gravity for death animation
    private Vector3 velocity;
    public float gravity = -9.81f;
    private bool isGrounded = true;

    void Update()
    {
        // Apply gravity when dead (simple version without CharacterController)
        if (isDead && !isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;

            // Simple ground check
            if (transform.position.y <= 0f)
            {
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                isGrounded = true;
            }
        }

        if (isDead || hasWon) return;

        // Get current light state
        string state = lightController.GetState();

        // React when state changes
        if (state != lastState)
        {
            lastState = state;

            // Random death chance on each state change (adds unpredictability)
            if (Random.Range(0f, 100f) < randomDeathChance)
            {
                Die();
                return;
            }

            if (state == "Green")
                OnGreen();
            else if (state == "Red")
                OnRed();
            else if (state == "Yellow")
                OnYellow();
        }

        // Continuous forward movement
        if (animator.GetFloat("Blend") > 0f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // Check if NPC reached finish line (you'll need to set this up)
        CheckWinCondition();
    }

    // GREEN - Run forward
    void OnGreen()
    {
        animator.SetFloat("Blend", 1f);
        animator.SetBool("Dance", false);
    }

    // YELLOW - Should dance (or make mistake)
    void OnYellow()
    {
        bool mistake = Random.Range(0f, 100f) > accuracy;

        if (!mistake)
        {
            // Correct behavior: Dance
            animator.SetFloat("Blend", 0f);
            animator.SetBool("Dance", true);
        }
        else
        {
            // Mistake: Continue moving or stay idle instead of dancing
            animator.SetBool("Dance", false);
            animator.SetFloat("Blend", Random.value > 0.5f ? 1f : 0f);
            Die();
        }
    }

    // RED - Must stop completely
    void OnRed()
    {
        bool mistake = Random.Range(0f, 100f) > accuracy;

        if (!mistake)
        {
            // Correct behavior: Stop everything
            animator.SetFloat("Blend", 0f);
            animator.SetBool("Dance", false);
        }
        else
        {
            // Mistake: Move or dance during red light
            if (Random.value > 0.5f)
            {
                animator.SetFloat("Blend", 1f);
            }
            else
            {
                animator.SetBool("Dance", true);
            }
            Die();
        }
    }

    // Check if NPC reached finish line
    void CheckWinCondition()
    {
        // Option 1: Distance-based win
        // If NPC travels far enough, they win
        if (transform.position.z >= 50f) // Adjust this value to your finish line position
        {
            Win();
        }

        // Option 2: Trigger-based win (if you have a finish line trigger)
        // You can use OnTriggerEnter instead (see below)
    }

    // Win
    void Win()
    {
        if (hasWon || isDead) return;

        hasWon = true;
        animator.SetFloat("Blend", 0f);
        animator.SetBool("Dance", true); // Celebrate!

        Debug.Log($"{gameObject.name} won!");

        // Optional: Destroy NPC after a delay
        // Destroy(gameObject, 3f);
    }

    // Death
    void Die()
    {
        if (isDead || hasWon) return;

        isDead = true;
        isGrounded = false; // Enable gravity fall
        animator.SetTrigger("Death");
        animator.SetBool("Dance", false);
        animator.SetFloat("Blend", 0f);
        animator.speed = 1f;

        Debug.Log($"{gameObject.name} died!");

        // Optional: Destroy NPC after death animation
        // Destroy(gameObject, 5f);
    }

    // Optional: If you have a finish line with a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            Win();
        }
    }
}