using System;
using Unity.Properties;
using UnityEngine;

public class AlertManager : Core.Singleton<AlertManager>
{
    [SerializeField] private AlertProfile profile;

    public event Action<AlertState> OnAlertStateChanged;
    [CreateProperty] public float CurrentLevel { get; set; } = 0f;
    [CreateProperty] public AlertState CurrentState => _currentState;

    private AlertState _currentState = AlertState.Clear;

    // public AlertTracker Tracker { get; private set; } = new AlertTracker();

    private const float MaxLevel = 100f;

    public void RaiseAlert(float amount)
    {
        CurrentLevel = Mathf.Clamp(CurrentLevel + amount, 0f, MaxLevel);
        // Tracker.PeakAlertLevel = Mathf.Max(Tracker.PeakAlertLevel, _currentLevel);
        // Tracker.AlertRiseCount++;
        UpdateState();
    }

    public void ReleaseAlert(float amount)
    {
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
        if (profile == null)
        {
            Debug.LogWarning($"No Alert profile. {CurrentState} remains.");
            return CurrentState;
        }
        else
        {
            if (level < profile.ClearUpper) return AlertState.Clear;
            if (level < profile.WatchedUpper) return AlertState.Watched;
            if (level < profile.SuspiciousUpper) return AlertState.Suspicious;
            if (level < profile.HotUpper) return AlertState.Hot;
            return AlertState.Caught;
        }

    }

    public void Reset()
    {
        CurrentLevel = 0f;
        _currentState = AlertState.Clear;
        // Tracker = new AlertTracker();
    }

    private void Update()
    {
        if (profile == null) return;

        if (_currentState == AlertState.Caught) return; // no decay once Caught
        CurrentLevel = Mathf.Max(0f, CurrentLevel - profile.DownRatePerSecond * Time.deltaTime);

        UpdateState();
    }
}
