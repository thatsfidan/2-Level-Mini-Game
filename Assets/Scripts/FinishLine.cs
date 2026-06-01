using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public UI gameUI;   // <-- reference to UI Manager
    public float delayBeforeNextLevel = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level 1 completed!");

            if (gameUI != null)
            {
                gameUI.ShowWin();   // <-- correct way to show WIN text
            }
            else
            {
                Debug.LogError("FinishLine: gameUI reference is MISSING!");
            }

            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(delayBeforeNextLevel);
        SceneManager.LoadScene("Level2");
    }
}
