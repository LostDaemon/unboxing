using System;
using UnityEngine;
using Zenject;

public class LootSceneController : MonoBehaviour
{
    [SerializeField]
    private IndicatorController _moneyIndicator;

    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnEnable()
    {
        _gameManager.OnScoreChange += (value) => OnScoreChange(value);
    }

    private void OnDisable()
    {
        _gameManager.OnScoreChange -= (value) => OnScoreChange(value);
    }

    private void OnScoreChange(int value)
    {
        _moneyIndicator.Value = value;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
