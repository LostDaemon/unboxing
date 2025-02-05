using System;
using Core.Items;
using Zenject;

public class OfferEvent : BaseEvent
{
    public Item Item { get; }
    public float OfferCost { get; }

    private GameManager _gameManager;

    public OfferEvent(Item item, float offerCost, DateTime scheduledTime, GameManager gameManager) : base(scheduledTime)
    {
        _gameManager = gameManager;
        Item = item;
        OfferCost = offerCost;
    }

    public override void Invoke()
    {
        UnityEngine.Debug.Log("Event invoked");
        _gameManager.AddMoney(OfferCost);
        _gameManager.RemoveLoot(Item);
        Resolve();
    }
}
