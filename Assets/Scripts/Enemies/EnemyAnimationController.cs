using System;
using System.Collections;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Collider2D _attackCollider;

    private const string AttackName = "Attack";
    private const string RunName = "Run";
    private const string IdleName = "Idle";
    private const string DeathName = "Death";
    private bool _isAttacking;

    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void AttackAnimation()
    {
        if(_isAttacking) return;
        StartCoroutine(AttackCallback());
    }
    private IEnumerator AttackCallback()
    {
        _isAttacking = true;
        _animator.Play(AttackName);
        _attackCollider.enabled = true;
        yield return null;
        var clipLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(clipLength);
        _attackCollider.enabled = false;
        _isAttacking = false;
    }
    public void IdleAnimation()
    {
        _animator.Play(IdleName);
    }
    public void RunAnimation()
    {
        _animator.Play(RunName);
    }
    public void DeathAnimation(Action callback)
    {
        StartCoroutine(DeathCallback(callback));
    }
    private IEnumerator DeathCallback(Action callback)
    {
        _animator.Play(DeathName);
        yield return null;
        var clipLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(clipLength);
        callback.Invoke();
    }
}