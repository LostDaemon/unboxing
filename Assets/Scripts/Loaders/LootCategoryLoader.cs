using Core.Items;
using UnityEngine;
using Zenject;

public class LootCategoryLoader : MonoBehaviour
{
    public LootCategoryScriptableObject[] LootCategories;

    private LootCategoriesRepository _lootCategoriesRepository;

    [Inject]
    public void Construct(LootCategoriesRepository lootCategoriesRepository)
    {
        _lootCategoriesRepository = lootCategoriesRepository;
    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        foreach (var category in LootCategories)
        {
            _lootCategoriesRepository.Register(category);
        }
    }
}