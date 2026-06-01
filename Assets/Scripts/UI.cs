using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI winText;

    private void Start()
    {
        if (deathText != null)
        {
            deathText.gameObject.SetActive(false);
            // Ensure text has content and proper styling
            deathText.text = "GAME OVER!";
            deathText.fontSize = 60;
            deathText.color = Color.red;
            deathText.alignment = TextAlignmentOptions.Center;
        }

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
            winText.text = "YOU WIN!";
            winText.fontSize = 60;
            winText.color = Color.green;
            winText.alignment = TextAlignmentOptions.Center;
        }
    }

    public void ShowDeath()
    {
        Debug.Log("ShowDeath() CALLED");

        if (deathText != null)
        {
            deathText.gameObject.SetActive(true);
            Debug.Log($"Death text active: {deathText.gameObject.activeSelf}");
            Debug.Log($"Death text content: '{deathText.text}'");
            Debug.Log($"Death text color: {deathText.color}");
        }
        else
        {
            Debug.LogError("deathText is NULL!");
        }
    }

    public void ShowWin()
    {
        Debug.Log("ShowWin() CALLED");

        if (winText != null)
        {
            winText.gameObject.SetActive(true);
        }
    }
}