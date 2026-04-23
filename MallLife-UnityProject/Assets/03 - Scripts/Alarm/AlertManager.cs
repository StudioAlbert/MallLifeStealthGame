using System;
using Unity.Properties;
using UnityEngine;

public class AlertManager : Core.Singleton<AlertManager>
{
    [SerializeField] private AlertProfileSO _profileSO;

    public event Action<AlertState> OnAlertStateChanged;
    // ReSharper disable once MemberCanBePrivate.Global because of UI Binding
    [CreateProperty] public float CurrentLevel { get; set; } = 0f;
    // ReSharper disable once MemberCanBePrivate.Global because of UI Binding
    [CreateProperty] public AlertState CurrentState => _currentState;

    private AlertState _currentState = AlertState.Clear;

    // public AlertTracker Tracker { get; private set; } = new AlertTracker();

    private const float MaxLevel = 100f;

    private void Update()
    {
        if (_profileSO)
        {
            ReleaseAlert(_profileSO.DownRatePerSecond * Time.deltaTime);
        }
    }
    
    public void RaiseAlert(float amount)
    {
        CurrentLevel = Mathf.Clamp(CurrentLevel + amount, 0f, MaxLevel);
        // Tracker.PeakAlertLevel = Mathf.Max(Tracker.PeakAlertLevel, _currentLevel);
        // Tracker.AlertRiseCount++;
        UpdateState();
    }

    public void ReleaseAlert(float amount)
    {
        if (_currentState == AlertState.Caught) return; // no decay once Caught

        CurrentLevel = Mathf.Clamp(CurrentLevel - amount, 0f, MaxLevel);
        UpdateState();
    }

    private void UpdateState()
    {
        var previous = _currentState;
        _currentState = ComputeState(CurrentLevel);

        // if (_currentState >= AlertLevel.Hot)
        //     Tracker.ReachedHotOrCaught = true;

        if (previous != _currentState)
            OnAlertStateChanged?.Invoke(_currentState); // event pour le HUD/IA
    }

    private AlertState ComputeState(float level)
    {
        if (!_profileSO)
        {
            Debug.LogWarning($"No Alert profile. {CurrentState} remains.");
            return CurrentState;
        }

        if (level <= _profileSO.ClearUpper) return AlertState.Clear;
        if (level <= _profileSO.WatchedUpper) return AlertState.Watched;
        if (level <= _profileSO.SuspiciousUpper) return AlertState.Suspicious;
        if (level <= _profileSO.HotUpper) return AlertState.Hot;
        return AlertState.Caught;

    }

    public void Reset()
    {
        CurrentLevel = 0f;
        _currentState = AlertState.Clear;
        // Tracker = new AlertTracker();
    }

    
}
