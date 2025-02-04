using System.Collections.Generic;
using Core.Items;

public class GameManager
{
    private readonly GameSceneManager _gameSceneManager;
    public delegate void MoneyChangeArgs(int score);
    public event MoneyChangeArgs OnMoneyChange;

    public delegate void LootChangeArgs();
    public event LootChangeArgs OnLootChange;

    public int TotalMoney { get; private set; }
    public int CurrentMoney { get; private set; }

    private List<Item> _loot;

    public GameManager(GameSceneManager gameSceneManager)
    {
        _gameSceneManager = gameSceneManager;
        _loot = new List<Item>();
    }

    public void AddMoney(int score = 0)
    {
        CurrentMoney += score;
        OnMoneyChange?.Invoke(CurrentMoney);
    }

    public void Win()
    {
        _gameSceneManager.LoadWinScene();
        TotalMoney += CurrentMoney;
        ResetCurrentMoney();
    }

    public void Loose()
    {
        _gameSceneManager.LoadLooseScene();
        ResetCurrentMoney();
    }

    public void AddLoot(Item loot)
    {
        _loot.Add(loot);
        OnLootChange?.Invoke();
    }

    public void RemoveLoot(Item loot)
    {
        _loot.Remove(loot);
        OnLootChange?.Invoke();
    }

    public IReadOnlyList<Item> GetLoot() => _loot;

    private void ResetCurrentMoney()
    {
        CurrentMoney = 0;
    }

    private void ResetTotalMoney()
    {
        TotalMoney = 0;
    }




}
