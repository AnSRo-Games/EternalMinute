using UnityEngine;
using UnityEngine.InputSystem;

public class IntroManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; // Pause the game
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void LoadGame()
    {
        // Load the main game scene
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        UnityEngine.SceneManagement.SceneManager.LoadScene("1st Scene");
    }
}
