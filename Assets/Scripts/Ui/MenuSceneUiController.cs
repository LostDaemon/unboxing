using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class MenuSceneUiController : MonoBehaviour
{
    private VisualElement _root;
    private Button _newGameButton;
    private Button _aboutButton;

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

        _newGameButton = _root.Q<Button>(nameof(_newGameButton));
        _newGameButton.clicked += () => _sceneManager.LoadLootScene();

        _aboutButton = _root.Q<Button>(nameof(_aboutButton));
        _aboutButton.clicked += () => Debug.Log("About clicked!");
    }

    private void UnRegister()
    {
        _newGameButton.clicked -= () => _sceneManager.LoadLootScene(); ;
        _aboutButton.clicked -= () => Debug.Log("About clicked!");
    }
}