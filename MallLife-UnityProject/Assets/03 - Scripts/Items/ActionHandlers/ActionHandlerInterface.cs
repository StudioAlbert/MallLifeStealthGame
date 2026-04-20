using System;
using UnityEngine;

public enum ActionResult
{
    Green,
    Yellow,
    Red
}

public interface IActionHandler
{
    void Init(GameObject actionObject, Action<bool> onComplete);
    void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs);
    
    IUIActionView UIActionView { get; }
}

public interface IQTEHandler : IActionHandler
{
    void Init(GameObject actionObject, Action<ActionResult> onComplete);
}
