using UnityEngine;
using Zenject;

public class PlayerHitBox : MonoBehaviour
{
    private PlayerFacade _player;
    [Inject]
    public void Construct(PlayerFacade player)
    {
        _player = player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyHitArea>(out var enemy))
        {
            _player.ChangeHealth(-10);
        }
    }
}
