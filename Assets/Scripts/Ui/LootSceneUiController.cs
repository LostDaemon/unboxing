using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LootSceneUiController : MonoBehaviour
{
    [SerializeField] public VisualTreeAsset itemTemplate;
    private VisualElement _parentContainer;
    private VisualElement _root;
    private Label _scoreLabel;

    private GameManager _gameManager;
    private RewardService _rewardService;

    [Inject]
    public void Construct(GameManager gameManager, RewardService rewardService)
    {
        _gameManager = gameManager;
        _rewardService = rewardService;
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
        _parentContainer = _root.Q<VisualElement>("lootContainer");
        _scoreLabel = _root.Q<Label>(nameof(_scoreLabel));
        _gameManager.OnScoreChange += OnScoreChanged;
        _rewardService.OnGetReward += OnGetReward;
    }

    private void OnGetReward(LootScriptableObject reward)
    {
        CreateNewItem(reward);
    }

    private void OnScoreChanged(int score)
    {
        _scoreLabel.text = $"${score}";
    }

    private void UnRegister()
    {
        _gameManager.OnScoreChange -= OnScoreChanged;
        _rewardService.OnGetReward -= OnGetReward;
    }

    private void CreateNewItem(LootScriptableObject item)
    {
        var newItem = itemTemplate.CloneTree();
        var lootElement = new VisualElement();
        lootElement.AddToClassList("loot-element");
        lootElement.style.backgroundImage = new StyleBackground(item.Image);
        _parentContainer.Add(newItem);
        StartCoroutine(RemoveElementAfterDelay(newItem, 5f));
    }

    private IEnumerator RemoveElementAfterDelay(VisualElement element, float delay)
    {
        yield return new WaitForSeconds(delay);
        _parentContainer.Remove(element);
    }
}