
using System.Linq;
using Core.Items;
using Zenject;

public sealed class RewardService
{
    private readonly ItemsFactory _itemsFactory;
    public delegate void RewardArguments(Item reward);
    public event RewardArguments OnGetReward;

    private readonly LootRepository _lootRepository;

    [Inject]
    public RewardService(LootRepository lootRepository, ItemsFactory itemsFactory)
    {
        _lootRepository = lootRepository;
        _itemsFactory = itemsFactory;
    }

    public void GetReward(ItemType type)
    {
        var typedItems = _lootRepository
        .Get()
        .Where(c => c.ItemType == type)
        .ToList();

        if (!typedItems.Any())
        {
            return;
        }

        var index = UnityEngine.Random.Range(0, typedItems.Count());
        var item = _itemsFactory.Create(typedItems[index]);
        OnGetReward?.Invoke(item);
    }
}
