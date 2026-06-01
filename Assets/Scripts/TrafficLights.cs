using UnityEngine;
using System.Collections;

public class trafficlights : MonoBehaviour
{
    public GameObject greenLight;
    public GameObject redLight;
    public GameObject yellowLight;

    public string currentState; //stores which balloon is active 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LightCycle());// Starts the coroutine that loops balloon colors
    }

    // Coroutine that changes balloon states repeatedly with random delays
    IEnumerator LightCycle()
    {
        while (true)
        {
            SetState("Green"); // Turn on green balloon
            yield return new WaitForSeconds(Random.Range(3f, 8f)); // Wait 3–8 seconds

            SetState("Red");
            yield return new WaitForSeconds(Random.Range(3f, 8f));

            SetState("Yellow");
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }

    // Turns ONE balloon on and the other two off
    void SetState(string state)
    {
        currentState = state;  // Save the current state so NPCs/players can check it

        // Activate only the balloon that matches the state
        greenLight.SetActive(state == "Green");  // True = show green, False = hide
        redLight.SetActive(state == "Red");
        yellowLight.SetActive(state == "Yellow");
    }

    // Other scripts call this to know which balloon is active
    public string GetState()
    {
        return currentState;  // Returns "Green", "Red", or "Yellow"
    }
    // Update is called once per frame
    void Update()
    {

    }
}
