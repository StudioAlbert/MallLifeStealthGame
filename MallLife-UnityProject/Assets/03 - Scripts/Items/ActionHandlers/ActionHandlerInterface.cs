using System;
using UnityEngine;

public enum ActionResult
{
    Success,
    MidTierResult,
    Failed
}

public interface IQTEHandler
{
    void Init(GameObject actionObject, Action<ActionResult> onComplete);
    void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs);
    
    IUIQteView UIActionView { get; }
}
