using System.Collections;
using Core.Items;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LootSceneUiController : MonoBehaviour
{
    [SerializeField] private LootItemUiController _itemPrefab;

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
        _rewardService.OnGetReward += OnGetReward;
    }

    private void OnGetReward(Item reward)
    {
        CreateNewItem(reward);
    }

    private void UnRegister()
    {
        _rewardService.OnGetReward -= OnGetReward;
    }

    private void CreateNewItem(Item item)
    {
        var newItem = Instantiate(_itemPrefab, this.transform) as LootItemUiController;
        newItem.Model = item;
        StartCoroutine(RemoveElementAfterDelay(newItem.gameObject, 5f));
    }

    private IEnumerator RemoveElementAfterDelay(GameObject element, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(element);
    }
}