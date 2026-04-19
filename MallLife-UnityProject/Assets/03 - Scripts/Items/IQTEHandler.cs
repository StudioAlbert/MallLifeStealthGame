using System;
using UnityEngine;

public interface IQTEHandler
{
    void Init(GameObject actionObject, Action<bool> onComplete);
    void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs);
    
    public bool NeedToConfirm { get; }
    
    IUIView UIView { get; }
}
