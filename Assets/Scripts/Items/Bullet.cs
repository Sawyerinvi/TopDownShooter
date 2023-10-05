using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;
    public float Damage => _damage;
    private void Awake()
    {
        StartCoroutine(BulletDeath());
    }
    private IEnumerator BulletDeath()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
