using System;
using UnityEngine;

public class StepTracker : MonoBehaviour
{
    [SerializeField] private float _stepDistance = 0.7f;

    private Vector3 _lastPosition;
    private float _distanceTraveled;

    public event Action OnStep;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _lastPosition);
        _distanceTraveled += distance;

        if (_distanceTraveled >= _stepDistance)
        {
            OnStep?.Invoke();
            _distanceTraveled = 0f;
        }

        _lastPosition = transform.position;
    }


}