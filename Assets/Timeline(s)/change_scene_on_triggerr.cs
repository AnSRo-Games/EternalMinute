using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene_on_trigger : MonoBehaviour
{
    public string sceneName;
    private void functionthatactivateswhentheplayerobtainsthelighter()
    {
            SceneManager.LoadScene(sceneName);
    }
}


