using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, ICollectable
{
    public int Value;
    public Rigidbody2D Rigidbody2D { get; set; }
    public Collider2D Collider2D { get; set; }

    void Awake() {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

    public void Collect()
    {
        BattleManager.s_Instance.AddMoney(Value);
        Destroy(gameObject);
    }

    public static Coin Create(Vector2 position, Vector2 velocity, int value)
    {
        Coin coin = Instantiate(BattleManager.s_Instance.CoinPrefab, position, Quaternion.identity);
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector2 randomVelocity = randomDirection.normalized * Random.Range(3, 20f);
        coin.Rigidbody2D.velocity = velocity + randomVelocity;
        coin.Value = value;
        return coin;
    }
}