using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    private Transform _transform;

    [Inject]
    private void Construct(PlayerController playerController)
    {
        _transform = playerController.transform;
    }

    void Start()
    {
        
    }

    private void Awake()
    {
        InitializeCameraFollow();
    }

    private void InitializeCameraFollow()
    {
        _camera.Follow = _transform;
    }

}
