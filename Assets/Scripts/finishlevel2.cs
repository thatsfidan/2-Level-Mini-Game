using UnityEngine;
using TMPro;

public class FinishTrigger : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public Animator playerAnimator;   // Assign your player’s animator here

    private void Start()
    {
        if (winText != null)
            winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show win text
            if (winText != null)
            {
                winText.gameObject.SetActive(true);
                winText.text = "YOU WON!";
            }

            // Play dance animation
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Dance");
            }

            Debug.Log("YOU WON – Dance triggered!");
        }
    }
}
