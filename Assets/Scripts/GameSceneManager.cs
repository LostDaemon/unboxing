using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuscene");
    }

    public void LoadLootScene()
    {
        SceneManager.LoadSceneAsync("LootScene");
    }

    public void LoadLoadingScene()
    {
        SceneManager.LoadSceneAsync("LoadingScene");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadSceneAsync("WinScene");
    }


}
