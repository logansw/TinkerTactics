using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] TMP_Text _healthText;
    [SerializeField] TMP_Text _moneyText;
    public Coin CoinPrefab;

    private int _health;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            _healthText.text = $"Health: {_health}";
            if (_health <= 0)
            {
                StateController.s_Instance.ChangeState(StateType.LossState);
            }
        }
    }

    private int _money;
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            _moneyText.text = $"Money: {_money}";
        }
    }

    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.BuyState);
    }

    void OnBattleStart()
    {
        Health = 100;
        Money = 0;
    }

    void OnEnable()
    {
        BattleState.e_OnBattleStart += OnBattleStart;
    }

    void OnDisable()
    {
        BattleState.e_OnBattleStart -= OnBattleStart;
    }
    
    public void Continue()
    {
        if (StateController.s_Instance.CurrentState == StateType.PreStartState)
        {
            // TODO:
        }
        else if (StateController.s_Instance.CurrentState == StateType.BuyState)
        {
            StateController.s_Instance.ChangeState(StateType.BattleState);
        }
        else if (StateController.s_Instance.CurrentState == StateType.BattleState)
        {
            StateController.s_Instance.ChangeState(StateType.BuyState);
        }
        else if (StateController.s_Instance.CurrentState == StateType.VictoryState)
        {
            // TODO: Move to the next level
        }
        else if (StateController.s_Instance.CurrentState == StateType.LossState)
        {
            // TODO: Restart the level
        }
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public void ChangeHealth(int amount)
    {
        Health += amount;
    }
}
