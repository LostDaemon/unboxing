using System;
using System.Collections.Generic;
using Core;
using Core.Items;
using UnityEngine;
using Zenject;

public class MetaGameplaySceneController : MonoBehaviour
{
    private GameManager _gameManager;
    private EventScheduler _eventScheduler;
    private TimeManager _timeManager;

    [SerializeField]
    private GameObject _itemsContainer;

    [SerializeField]
    private IndicatorController _moneyIndicator;

    [SerializeField]
    private LootItemUiRowController _itemsPrefab;

    private List<LootItemUiRowController> _items;


    [Inject]
    public void Construct(GameManager gameManager, EventScheduler eventScheduler, TimeManager timeManager)
    {
        _gameManager = gameManager;
        _eventScheduler = eventScheduler;
        _timeManager = timeManager;
        _gameManager.OnMoneyChange += (args) => OnMoneyChange(args);
        _gameManager.OnLootChange += () => OnLootChange();
        _items = new List<LootItemUiRowController>();
    }

    private void OnMoneyChange(float amount)
    {
        _moneyIndicator.Value = amount;
    }

    private void OnLootChange()
    {
        UpdateList();
    }


    void Start()
    {
        UpdateList();
    }

    private void OnDisable()
    {
        _items.ForEach(item => item.OnOfferCreate -= (sender) => OnOfferCreate(sender));
        _gameManager.OnMoneyChange -= (args) => OnMoneyChange(args);
        _gameManager.OnLootChange -= () => OnLootChange();
    }

    private void OnOfferCreate(LootItemUiRowController sender)
    {
        var item = sender.Model;
        var cost = item.Cost * RarityCostMultiplier.GetCostMultiplier(item.Rarity);
        var expirationTime = _timeManager.CurrentIngameTime.AddSeconds(5);
        var offerEvent = new OfferEvent(item, cost, expirationTime, _gameManager);
        _eventScheduler.RegisterEvent(offerEvent);
    }

    private void UpdateList()
    {
        _itemsContainer.transform.DeletChildren();
        var lootItems = _gameManager.GetLoot();
        foreach (var lootItem in lootItems)
        {
            var item = Instantiate(_itemsPrefab, _itemsContainer.transform);
            _items.Add(item);
            item.Model = lootItem;
            item.OnOfferCreate += (sender) => OnOfferCreate(sender);
        }
    }
}
