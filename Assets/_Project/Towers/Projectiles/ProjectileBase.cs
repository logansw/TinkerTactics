using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [HideInInspector] public Tower SourceTower { get; protected set; }
    [HideInInspector] public float Damage;
    private SpriteRenderer _renderer;
    private Collider2D _collider;
    public ProjectileAttributeTracker ProjectileAttributeTracker { get; protected set; }
    protected bool _isStale;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_isStale) { return; }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnImpact(enemy);
        }
    }

    protected abstract void Move();
    protected virtual void OnImpact(Enemy recipient)
    {
        EventBus.RaiseEvent<PreEnemyImpactEvent>(new PreEnemyImpactEvent(recipient, this));
        recipient.ReceiveDamage(Damage, this, SourceTower);
        EventBus.RaiseEvent<PostEnemyImpactEvent>(new PostEnemyImpactEvent(recipient, this));
        if (ProjectileAttributeTracker.HasAttribute<PierceProjectileAttribute>(out PierceProjectileAttribute pierceProjectileAttribute))
        {
            if (pierceProjectileAttribute.Stacks < 0)
            {
                _isStale = true;
                CleanUp();
            }
        }
        else
        {
            CleanUp();
        }
    }

    public void CleanUp()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
        Destroy(gameObject, 1f);
    }
}