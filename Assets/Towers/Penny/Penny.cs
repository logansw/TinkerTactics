using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PennyLauncher))]
public class Penny : Tower
{
    public float GoldCollected;
    public int GoldCollectionCapacity;
    public float GoldCollectionRate;
    private float _moneyStacksAttackSpeedBonusRate = 0f;
    private int _moneyStacksADBonusRate = 0;
    private int _moneyStacksGoldThreshold = 10;
    PennyLauncher pennyLauncher;

    protected override void Awake()
    {
        base.Awake();
        pennyLauncher = GetComponent<PennyLauncher>();
        Upgrades = new List<List<Upgrade>>();
        List<Upgrade> tierOneUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Name = "Faster Collection",
                Description = "Increases the rate at which gold is collected by 0.1.",
                Cost = 100,
                UpgradeEffect = () => { GoldCollectionRate += 0.1f; }
            },
            new Upgrade
            {
                Name = "Money Stacks",
                Description = "Gain 10% AS and 5 AD for every 10 gold in the bank.",
                Cost = 100,
                UpgradeEffect = () =>
                {
                    _moneyStacksAttackSpeedBonusRate = 0.10f;
                    _moneyStacksADBonusRate = 5;
                }
            }
        };
        List<Upgrade> tierTwoUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Name = "Bigger Bank",
                Description = "Increases the amount of gold that can be stored by 50.",
                Cost = 100,
                UpgradeEffect = () => { GoldCollectionCapacity += 50; }
            }
        };
        Upgrades.Add(tierOneUpgrades);
        Upgrades.Add(tierTwoUpgrades);
    }

    protected override void Start()
    {
        base.Start();
        GoldCollectionCapacity = 50;
        GoldCollectionRate = 0.1f;
    }

    protected override void Attack()
    {
        pennyLauncher.LaunchProjectile(TargetTracker.GetHighestPriorityTarget());
    }

    private void OnEnable()
    {
        pennyLauncher.e_OnKill += AccumulateGold;
    }

    private void OnDisable()
    {
        pennyLauncher.e_OnKill -= AccumulateGold;
    }

    private void AccumulateGold(Enemy enemy)
    {
        GoldCollected += enemy.GoldValue * GoldCollectionRate;
        int MoneyStacks = (int)GoldCollected / _moneyStacksGoldThreshold;
        pennyLauncher.Damage = pennyLauncher.BaseDamage + MoneyStacks * _moneyStacksADBonusRate;
        pennyLauncher.AttackSpeed = pennyLauncher.BaseAttackSpeed + (MoneyStacks * _moneyStacksAttackSpeedBonusRate * pennyLauncher.BaseAttackSpeed);
    }
}