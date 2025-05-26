using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class SceneChangerAfterThreePresses : MonoBehaviour
{
    public string sceneToLoad = "NextScene"; // Replace with your actual scene name
    private int spacePressCount = 0;
    public int N;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressCount++;
            Debug.Log("Space pressed " + spacePressCount + " times.");

            if (spacePressCount >= N)
            {
                Debug.Log("Loading scene: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
