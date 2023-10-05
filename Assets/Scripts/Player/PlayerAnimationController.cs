using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string RunAnimationName = "Run";
    private const string IdleAnimationName = "Idle";
    private const string ShootAK74Name = "Shoot AK-74";
    private const string ShootMakarovName = "Shoot Makarov";
        
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerAudioManager _audioManager;

    private bool _isShooting;

    public void RunAnimation()
    {
        _animator.Play(RunAnimationName);
    }

    public void IdleAnimation()
    {
        _animator.Play(IdleAnimationName);
    }

    public void FireAnimation(Action callback)
    {
        if(_isShooting) return;
        _audioManager.ShootAK74();
        StartCoroutine(Shooting(callback));
    }

    private IEnumerator Shooting(Action callback)
    {
        _isShooting = true;
        _animator.Play(ShootAK74Name);
        yield return null;
        var clipLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(clipLength);
        callback();
        _isShooting = false;
    }
    
    
}
