using System;
using UnityEngine;

public class ActionHandlerPush : MonoBehaviour, IQTEHandler 
{
    [Header("References")]
    [SerializeField] private UIActionViewPush _itemUIActionView;
        
    [Header("UI Behaviour")]
    [SerializeField] private bool _needToConfirm;
    [SerializeField] private bool _autoClose;
    
    public IUIQteView UIActionView => _itemUIActionView;
    public bool NeedToConfirm => _needToConfirm;
    public bool AutoClose => _autoClose;

    private float _totalTickTime;
    private SimplePushDescriptor _descriptor;
    private Action<ActionResult> _handlerDone;


    // Fix this with UI, here is some range placeholder
    public float ErrorRatio => _totalTickTime / _descriptor.TotalTime;
    
    public void Init(GameObject actionObject, Action<ActionResult> handlerDone)
    {
        _handlerDone = handlerDone;
        _totalTickTime = 0;
        // _worldPlacement.ToFollow = actionObject.transform;
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIActionView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as SimplePushDescriptor;
        }
    }
    
    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if(inputs.SouthBtnDown)
            _handlerDone?.Invoke(ActionResult.Success);
        
        if(_totalTickTime >= _descriptor.TotalTime)
            _handlerDone?.Invoke(ActionResult.Success);
        
        _totalTickTime += deltaTime;
        
        // just maintain A, always success, no failure on which button done
        // Debug.Log($"Waiting a push : {_totalTickTime}/{ErrorRatio}");
        _itemUIActionView.SetTotalRatio(ErrorRatio);
        
    }
    
}
