
using System.Linq;
using Zenject;

public sealed class RewardService
{
    public delegate void RewardArguments(LootScriptableObject reward);
    public event RewardArguments OnGetReward;

    private readonly LootRepository _lootRepository;

    [Inject]
    public RewardService(LootRepository lootRepository)
    {
        _lootRepository = lootRepository;
    }

    public void GetReward(ItemTypes type)
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
        var loot = typedItems[index];
        OnGetReward?.Invoke(loot);
    }
}
