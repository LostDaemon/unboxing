using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LootSceneUiController : MonoBehaviour
{
    private VisualElement _root;
    private Label _scoreLabel;

    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnEnable()
    {
        Register();
    }

    private void OnDisable()
    {
        UnRegister();
    }

    private void Register()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _scoreLabel = _root.Q<Label>(nameof(_scoreLabel));
        _gameManager.OnScoreChange += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreLabel.text = $"${score}";
    }

    private void UnRegister()
    {
        _gameManager.OnScoreChange -= OnScoreChanged;
    }
}