using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCameraBase _camera;
    void Start()
    {
        _camera = GetComponent<CinemachineVirtualCameraBase>();
        _camera.Follow = FindObjectOfType<PlayerCollision>().transform;
    }

    
}
