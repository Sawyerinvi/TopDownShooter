using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out var bullet))
        {
            _enemyStats.ApplyDamage(bullet.Damage);
            Destroy(bullet.gameObject);
        }
    }

}
