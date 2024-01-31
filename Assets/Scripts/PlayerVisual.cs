using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private bool _isMoving { get; set; }
    private Vector2 _motionVector { get; set; }

    private PlayerController _playerController;

    //[Inject]
    //private void Construct(PlayerController playerController)
    //{
    //    _playerController = playerController;
    //}

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("isMove", _playerController.IsMove());

        if (_playerController.IsMove())
        {
            _animator.SetFloat("directionX", _playerController.GetMotionVector().x);
            _animator.SetFloat("directionY", _playerController.GetMotionVector().y);
        }
    }
}
