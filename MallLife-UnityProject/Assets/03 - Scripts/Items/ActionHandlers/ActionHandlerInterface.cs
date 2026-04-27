using System;
using UnityEngine;

public enum ActionResult
{
    Success,
    MediumFailed,
    Failed
}

public interface IQTEHandler
{
    void Init(GameObject actionObject, Action<ActionResult> handlerDone);
    void Tick(float deltaTime, Inputs.QuickTimeEvents inputs);
    
    IUIQteView UIActionView { get; }
    public bool NeedToConfirm { get; }
    public bool AutoClose { get; }

}
