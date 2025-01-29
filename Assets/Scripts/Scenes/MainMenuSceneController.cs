using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public void LoadLootScene()
    {
        Debug.Log("Load Scene");
        SceneManager.LoadSceneAsync("LootScene", LoadSceneMode.Single);
    }
}
