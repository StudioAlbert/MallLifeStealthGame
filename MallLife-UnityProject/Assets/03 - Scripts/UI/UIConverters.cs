using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class UIConverters
{
    public static readonly ConverterGroup Alarm;

    static UIConverters()
    {
        Alarm = new ConverterGroup("Alarm");
        // Color converter
        Alarm.AddConverter((ref float v) => new StyleColor(Color.Lerp(Color.greenYellow, Color.crimson, Mathf.Clamp01(v / 100f)))
        );
        Alarm.AddConverter((ref float v) => new StyleLength(new Length(Mathf.Clamp(v, 0f, 100f), LengthUnit.Percent))
        );
        // CurrentLevel (float) -> integer string for display, rounded to nearest.
        Alarm.AddConverter((ref float v) => Mathf.RoundToInt(v).ToString());
        // AlertState -> human-readable label (UI-only concern, kept out of AlertManager).
        Alarm.AddConverter((ref AlertState s) => s switch
        {
            AlertState.Clear      => "Clear",
            AlertState.Watched    => "Watched",
            AlertState.Suspicious => "Suspicious",
            AlertState.Hot        => "Hot",
            AlertState.Caught     => "Caught",
            _                     => s.ToString()
        });
        // Register globally so UXML can reference it by name via source-to-ui-converters="Alarm".
        ConverterGroups.RegisterConverterGroup(Alarm);
        
        
        
    }

    // Ensures the static constructor runs before any scene loads, so the
    // ConverterGroup is registered before UI Toolkit tries to resolve it by name.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureRegisteredAtRuntime() { }

#if UNITY_EDITOR
    // Also register in the Editor so the ConverterGroup appears in UI Builder's
    // bindings dropdown (UI Builder runs in Edit mode, where
    // RuntimeInitializeOnLoadMethod does not fire).
    [InitializeOnLoadMethod]
    private static void EnsureRegisteredInEditor() { }
#endif  
}
