using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string NextScene;
    public int DelayInSeconds = 3;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > DelayInSeconds)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(NextScene);
    }
}
