public class GameManager
{
    private readonly GameSceneManager _gameSceneManager;
    public delegate void ScoreChangeArgs(int score);
    public event ScoreChangeArgs OnScoreChange;
    public int TotalScore { get; private set; }
    public int CurrentScore { get; private set; }

    public GameManager(GameSceneManager gameSceneManager)
    {
        _gameSceneManager = gameSceneManager;
    }

    public void AddScore(int score = 100)
    {
        CurrentScore += score;
        OnScoreChange?.Invoke(CurrentScore);
    }

    public void Win()
    {
        _gameSceneManager.LoadWinScene();
        TotalScore += CurrentScore;
        ResetCurrentScore();
    }

    public void Loose()
    {
        _gameSceneManager.LoadLooseScene();
        ResetCurrentScore();
    }

    private void ResetCurrentScore()
    {
        CurrentScore = 0;
    }

    private void ResetTotalScore()
    {
        TotalScore = 0;
    }
}
