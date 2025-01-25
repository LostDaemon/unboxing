using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuscene", LoadSceneMode.Single);
        UnityEngine.Debug.Log("MainMenuscene Loaded");
    }

    public void LoadLootScene()
    {
        SceneManager.LoadSceneAsync("LootScene", LoadSceneMode.Single);
        UnityEngine.Debug.Log("LootScene Loaded");
    }

    public void LoadLoadingScene()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
        UnityEngine.Debug.Log("LoadingScene Loaded");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadSceneAsync("WinScene", LoadSceneMode.Additive);
        UnityEngine.Debug.Log("WinScene Loaded");
    }

    public void LoadLooseScene()
    {
        SceneManager.LoadSceneAsync("LooseScene", LoadSceneMode.Additive);
        UnityEngine.Debug.Log("LooseScene Loaded");
    }

    public void LoadMetaGameplayScene()
    {
        SceneManager.LoadSceneAsync("MetaGameplayScene", LoadSceneMode.Single);
        UnityEngine.Debug.Log("MetaGameplayScene Loaded");
    }
}
