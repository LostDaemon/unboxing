using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class MetaGameplaySceneUiController : MonoBehaviour
{
    private VisualElement _root;
    private Button _exitButton;
    private Button _playButton;

    private GameSceneManager _sceneManager;

    [Inject]
    public void Construct(GameSceneManager sceneManager)
    {
        _sceneManager = sceneManager;
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
        _exitButton = _root.Q<Button>(nameof(_exitButton));
        _exitButton.clicked += () => _sceneManager.LoadMainMenuScene();
        _playButton = _root.Q<Button>(nameof(_playButton));
        _playButton.clicked += () => _sceneManager.LoadLootScene();
    }

    private void UnRegister()
    {
        _exitButton.clicked -= () => _sceneManager.LoadMainMenuScene();
        _playButton.clicked -= () => _sceneManager.LoadLootScene();
    }
}