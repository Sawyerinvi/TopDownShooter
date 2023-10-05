using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArea : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _steps;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _aim;

    private Vector3 _currentDirection = new Vector3(1, 0, 0);

    public Vector3 CurrentDirection => _currentDirection;

    
    void Start()
    {
        DrawCircle(_steps, _radius);
    }
    
    private void DrawCircle(int steps, float radius)
    {
        _lineRenderer.positionCount = steps;
        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            float progress = (float)currentStep / steps;
            float currentRadian = progress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x,y,0);
            _lineRenderer.SetPosition(currentStep, currentPosition);
        }
    }
    public void SetAimPosition(float vertical, float horizontal)
    {
        var direction = new Vector3(horizontal, vertical, 0).normalized;
        if (direction != Vector3.zero) _currentDirection = direction;
        _aim.transform.localPosition = _currentDirection * _radius * 0.9f;
        var angle = Mathf.Atan(_currentDirection.y / _currentDirection.x) * Mathf.Rad2Deg;
        _aim.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));

    }
}
