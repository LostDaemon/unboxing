using Zenject;

namespace Core.Items
{
    public sealed class ItemsFactory
    {
        private readonly LootRepository _lootRepository;

        [Inject]
        public ItemsFactory(LootRepository lootRepository)
        {
            _lootRepository = lootRepository;
        }

        public Item Create(LootScriptableObject prototype)
        {
            var rarity = RarityWeights.GetRandomRarity();
            var cost = prototype.Cost * RarityCostMultiplier.GetCostMultiplier(rarity);

            return new Item
            {
                Name = prototype.Name,
                ItemType = prototype.ItemType,
                Image = prototype.Image,
                Description = prototype.Description,
                Rarity = rarity,
                Cost = cost
            };
        }
    }

}