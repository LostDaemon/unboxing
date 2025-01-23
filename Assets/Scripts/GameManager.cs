public class GameManager
{
    public delegate void ScoreChangeArgs(int score);
    public event ScoreChangeArgs OnScoreChange;
    private int _score;
    private readonly GameSceneManager _gameSceneManager;
    public GameManager(GameSceneManager gameSceneManager)
    {
        _gameSceneManager = gameSceneManager;
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChange?.Invoke(_score);
        }
    }

    public void Win()
    {
        _gameSceneManager.LoadWinScene();
    }
}
