using UnityEngine;
using Zenject;

public class LootLoader : MonoBehaviour
{
    public LootScriptableObject[] LootItemsPrototypes;

    private LootRepository _lootRepository;

    [Inject]
    public void Construct(LootRepository lootRepository)
    {
        _lootRepository = lootRepository;
    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        foreach (var prototype in LootItemsPrototypes)
        {
            _lootRepository.Register(prototype);
        }
    }
}
