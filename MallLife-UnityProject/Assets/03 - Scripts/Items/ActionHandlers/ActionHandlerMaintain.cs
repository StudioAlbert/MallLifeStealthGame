using System;
using UnityEngine;

public class ActionHandlerMaintain : MonoBehaviour, IQTEHandler 
{
    [Header("References")]
    [SerializeField] private UIActionViewMaintain _itemUIActionView;
    // [SerializeField] private UIWorldPlacement _worldPlacement;

    public IUIQteView UIActionView => _itemUIActionView;
    
    private float _totalTickTime;
    private float _maintainedTime;
    private MaintainDescriptor _descriptor;
    private Action<ActionResult> _handlerDone;
    
    
    // Fix this with UI, here is some range placeholder
    private float ErrorRatio => _totalTickTime / _descriptor.FailTime;
    private float MaintainRatio => _maintainedTime / _descriptor.SuccessTime;
    
    public void Init(GameObject actionObject, Action<ActionResult> handlerDone)
    {
        _handlerDone = handlerDone;
        _totalTickTime = 0;
        _maintainedTime = 0;
        // _worldPlacement.ToFollow = actionObject.transform;
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIActionView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as MaintainDescriptor;
        }
    }
    
    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if(_maintainedTime >= _descriptor.SuccessTime)
            _handlerDone?.Invoke(ActionResult.Success);
        
        if(_totalTickTime >= _descriptor.FailTime)
            _handlerDone?.Invoke(ActionResult.Failed);
        
        _totalTickTime += deltaTime;
        if(inputs.SouthBtn) _maintainedTime += deltaTime;
        if(inputs.SouthBtnUp) _maintainedTime = 0;
        
        // just maintain A, always success, no failure on which button done
        Debug.Log($"Maintain is ticking : {_totalTickTime}/{ErrorRatio} , {_maintainedTime}/{MaintainRatio}");
        
        _itemUIActionView.SetTotalRatio(ErrorRatio);
        _itemUIActionView.SetActionRatio(MaintainRatio);
        
    }
}
