using UnityEngine;

[CreateAssetMenu(fileName = "AlertProfile", menuName = "Mall Life/Alert Profile")]
public class AlertProfile : ScriptableObject
{
    [Header("State thresholds (upper bound, exclusive)")]
    [Tooltip("level < this → Clear")]
    public float ClearUpper = 1f;
    [Tooltip("level < this → Watched")]
    public float WatchedUpper = 31f;
    [Tooltip("level < this → Suspicious")]
    public float SuspiciousUpper = 61f;
    [Tooltip("level < this → Hot; at or above → Caught")]
    public float HotUpper = 86f;

    [Header("Dynamics")]
    [Tooltip("Alert level eroded per second when nothing raises it. Decay is skipped while state is Caught.")]
    public float DownRatePerSecond = 5f;
    
}
