using System;
using Core.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LootSceneController : MonoBehaviour
{
    [SerializeField]
    private IndicatorController _moneyIndicator;

    private GameManager _gameManager;
    private RewardService _rewardService;

    [Inject]
    public void Construct(GameManager gameManager, RewardService rewardService)
    {
        _gameManager = gameManager;
        _rewardService = rewardService;
    }

    public void SkipScene()
    {
        SceneManager.LoadSceneAsync("WinScene");
    }

    private void OnEnable()
    {
        _gameManager.OnMoneyChange += (value) => OnScoreChange(value);
        _rewardService.OnGetReward += (item) => OnReward(item);
    }

    private void OnDisable()
    {
        _gameManager.OnMoneyChange -= (value) => OnScoreChange(value);
        _rewardService.OnGetReward -= (item) => OnReward(item);
    }

    private void OnScoreChange(float value)
    {
        _moneyIndicator.Value = value;
    }

    private void OnReward(Item item)
    {
        _gameManager.AddLoot(item);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
