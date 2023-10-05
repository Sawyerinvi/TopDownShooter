using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private ShootingArea _shootingArea;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed;

    public void Fire()
    {
        var bullet = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(_shootingArea.CurrentDirection.y, _shootingArea.CurrentDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.velocity = _shootingArea.CurrentDirection * _speed;

    }
}
