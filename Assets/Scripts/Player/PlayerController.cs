using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _body;
    [SerializeField] private Shooting _shooting;

    private PlayerAnimationController _animationController;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _animationController = GetComponent<PlayerAnimationController>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    private bool _isShooting;
    public void Move(float vertical, float horizontal)
    {
        if (horizontal > 0) _body.localScale = new Vector3(1, 1, 1);
        if (horizontal < 0) _body.localScale = new Vector3(-1, 1, 1);
        if (_isShooting == false)
        {
            Vector3 translate = (new Vector3(horizontal, vertical, 0) * Time.deltaTime) * _speed;
            _rigidbody.MovePosition(transform.position + translate);
            _animationController.RunAnimation();
        }
        else if(_isShooting == false)
        {
            _animationController.IdleAnimation();
        }
        
    }

    public void Attack()
    {
        if (_isShooting == false)
        {
            _shooting.Fire();
            _animationController.FireAnimation(ShootingCallback);
            _isShooting = true;
        }

    }
    private void ShootingCallback()
    {
         _isShooting = false;
    }
}
