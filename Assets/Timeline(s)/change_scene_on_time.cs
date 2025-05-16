using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene_on_time : MonoBehaviour
{
    public float changeTime;
    public string sceneName;
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
