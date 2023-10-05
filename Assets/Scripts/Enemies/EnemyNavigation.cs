using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _attackRange = 1.5f; 
    [SerializeField] float _chaseRange = 5f;
    [SerializeField] float _lootChance = 20;
    [SerializeField] Transform _body;
    [SerializeField] List<GameObject> _lootPrefabs = new List<GameObject>();

    private PlayerCollision _playerCollision;
    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private EnemyAnimationController _enemyAnimationController;
    private EnemyStats _enemyStats;
    private ItemFactory _itemFactory;
    private bool _isDying;

    [Inject]
    public void Construct(ItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyAnimationController = GetComponent<EnemyAnimationController>();
        _enemyStats = GetComponent<EnemyStats>();
    }
    private void Start()
    {
        _playerCollision = FindObjectOfType<PlayerCollision>();
        _playerTransform = _playerCollision.transform;
    }

    private void OnEnable()
    {
        _enemyStats.OnDeath += Death;
    }
    private void OnDisable()
    {
        _enemyStats.OnDeath -= Death;
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(_rb.position, _playerTransform.position);

        if(_isDying) return;

        if (distanceToPlayer <= _attackRange)
        {
            Attack();
            return;
        }
        if (distanceToPlayer <= _chaseRange)
        {
            MoveToTarget(_playerTransform.position);
            _enemyAnimationController.RunAnimation();
        }
        else
        {
            _enemyAnimationController.IdleAnimation();
        }

    }

    private void MoveToTarget(Vector2 targetPosition)
    {
        Vector2 targetDirection = (_rb.position - targetPosition).normalized;
        if(targetDirection.x < 0) _body.localScale = new Vector3(1, 1, 1);
        if (targetDirection.x > 0) _body.localScale = new Vector3(-1, 1, 1);

            RaycastHit2D hit = Physics2D.Raycast(_rb.position, targetDirection, _attackRange);

        if (hit.collider != null)
        {
            if (!hit.collider.TryGetComponent(out PlayerCollision playerCollision))
            {
                Vector2 newDirection = Vector2.Reflect(targetDirection, hit.normal);
                targetDirection = newDirection.normalized;
            }
        }

        Vector2 newPosition = _rb.position + targetDirection * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }
    private void Attack()
    {
        _enemyAnimationController.AttackAnimation();
    }
    private void Death()
    {
        _isDying = true;
        _enemyAnimationController.DeathAnimation(DropLoot);
    }
    private void DropLoot()
    {
        var random = Random.Range(0, 100);
        if(random < _lootChance)
        {
            var item = _lootPrefabs[Random.Range(0, _lootPrefabs.Count)];
            var loot = _itemFactory.Create(item);
            loot.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}

