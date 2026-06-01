using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public UI gameUI; // Your UI reference

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell into death zone!");

            // Get animator and play death
            Animator anim = other.GetComponent<Animator>();
            if (anim)
            {
                anim.SetTrigger("Death");
            }

            // Show game over
            if (gameUI != null)
            {
                gameUI.ShowDeath();
            }
        }
    }
}
