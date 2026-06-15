using System;
using UnityEngine;

public enum FearState
{
    Alone,
    Shadow,
    Light
}

public class FearSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StepTracker _stepTracker;
    [SerializeField] private PlayerAwareness _playerAwareness;

    [Header("Fear Settings")]
    [SerializeField] private float _fearValue;
    [SerializeField] private float _fearTriggerThreshold = 20f;

    private int _totalSteps;
    private FearState _currentState;

    public event Action OnFearThresholdReached;

    private void OnEnable()
    {
        _stepTracker.OnStep += HandleStep;
    }

    private void OnDisable()
    {
        _stepTracker.OnStep -= HandleStep;
    }

    private void Update()
    {
        UpdateFearState();
        UpdateFear();
        //Debug.Log($"Fear Value: {_fearValue}, State: {_currentState}");
        if (_fearValue >= _fearTriggerThreshold)
        {
            OnFearThresholdReached?.Invoke();

            // Tùy game design
            _fearValue = 0f;
        }
    }

    private void HandleStep()
    {
        _totalSteps++;

        // Ví dụ:
        // mỗi bước chân làm tăng fear một ít
        _fearValue += 0.2f;
    }

    private void UpdateFearState()
    {
        if (_playerAwareness.IsAlone())
        {
            _currentState = FearState.Alone;
            return;
        }
        
        if (_playerAwareness.IsInLight())
        {
                _currentState = FearState.Light;
                return;
        }

        _currentState = FearState.Shadow;
    }

    private void UpdateFear()
    {
        switch (_currentState)
        {
            case FearState.Alone:
                _fearValue += 1f * Time.deltaTime;
                break;

            case FearState.Shadow:
                _fearValue += 0.05f * Time.deltaTime;
                break;

            case FearState.Light:
                _fearValue -= 0.1f * Time.deltaTime;
                break;
        }

        _fearValue = Mathf.Clamp(_fearValue, 0f, 100f);
    }
}