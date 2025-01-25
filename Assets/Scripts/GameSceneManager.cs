using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuscene", LoadSceneMode.Single);
    }

    public void LoadLootScene()
    {
        SceneManager.LoadSceneAsync("LootScene", LoadSceneMode.Single);
    }

    public void LoadLoadingScene()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadSceneAsync("WinScene", LoadSceneMode.Additive);
    }

    public void LoadLooseScene()
    {
        SceneManager.LoadSceneAsync("LooseScene", LoadSceneMode.Additive);
    }

    public void LoadMetaGameplayScene()
    {
        SceneManager.LoadSceneAsync("MetaGameplayScene", LoadSceneMode.Single);
    }
}
