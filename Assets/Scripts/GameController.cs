using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public GameObject timesUpScreen;
    public string mainScene;
    public float timeLimit; // Set the time limit to 60 seconds
    private float minuteTimer = 0f;
    private float waitTimer = 0f;
    private bool isTimeUp = false;

    // Update is called once per frame
    void Update()
    {
        if (minuteTimer >= timeLimit)
        {
            TimesUp(); // Call the TimesUp method when one minute has passed
        } else {
            minuteTimer += Time.deltaTime;
        }

        if (isTimeUp)
        {
            // ToDo this on input later
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                restartGame(); // Restart the game after 3 seconds
            }
        }
    }

    void TimesUp()
    {
        Time.timeScale = 0f; // Pause the game
        timesUpScreen.SetActive(true); // Show the times up screen
        isTimeUp = true; // Set the flag to indicate time is up
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    public void restartGame()
    {
        SceneManager.LoadScene(mainScene); // Restart the current scene
        timesUpScreen.SetActive(false); // Hide the times up screen
        Time.timeScale = 1f; // Resume the game
        isTimeUp = false; // Reset the flag
        minuteTimer = 0f; // Reset the timer
        Cursor.visible = false; // Show the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
    }
}
