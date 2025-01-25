using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class WinSceneUiController : MonoBehaviour
{
    private VisualElement _root;
    private Button _continueButton;

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
        _continueButton = _root.Q<Button>(nameof(_continueButton));
        _continueButton.clicked += () => _sceneManager.LoadMetaGameplayScene();
    }

    private void UnRegister()
    {
        _continueButton.clicked -= () => _sceneManager.LoadMetaGameplayScene();
    }
}