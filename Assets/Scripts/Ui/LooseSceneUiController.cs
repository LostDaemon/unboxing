using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LooseSceneUiController : MonoBehaviour
{
    private VisualElement _root;
    private Button _exitButton;
    private Button _tryAgainButton;

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
        _exitButton.clicked += () => _sceneManager.LoadMetaGameplayScene();
        _tryAgainButton = _root.Q<Button>(nameof(_tryAgainButton));
        _tryAgainButton.clicked += () => _sceneManager.LoadLootScene();
    }

    private void UnRegister()
    {
        _exitButton.clicked -= () => _sceneManager.LoadMetaGameplayScene();
        _tryAgainButton.clicked += () => _sceneManager.LoadLootScene();
    }
}