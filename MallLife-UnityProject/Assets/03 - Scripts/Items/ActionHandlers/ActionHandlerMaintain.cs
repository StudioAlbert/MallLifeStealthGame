using System;
using UnityEngine;

public class ActionHandlerMaintain : MonoBehaviour, IQTEHandler 
{
    [Header("References")]
    [SerializeField] private UIActionViewMaintain _itemUIActionView;
    
    [Header("UI Behaviour")]
    [SerializeField] private bool _needToConfirm;
    [SerializeField] private bool _autoClose;
    
    public IUIQteView UIActionView => _itemUIActionView;
    public bool NeedToConfirm => _needToConfirm;
    public bool AutoClose => _autoClose;

    private float _totalTickTime;
    private float _maintainedTime;
    private MaintainDescriptorSO _descriptor;
    private Action<ActionResult> _handlerDone;


    // Fix this with UI, here is some range placeholder
    private float ErrorRatio => _totalTickTime / _descriptor.TotalTime;
    private float MaintainRatio => _maintainedTime / _descriptor.ActionTime;
    
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
            _descriptor = stealable.Descriptor as MaintainDescriptorSO;
        }
    }
    
    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if(_maintainedTime >= _descriptor.ActionTime)
            _handlerDone?.Invoke(ActionResult.Success);
        
        if(_totalTickTime >= _descriptor.TotalTime)
            _handlerDone?.Invoke(ActionResult.Failed);
        
        _totalTickTime += deltaTime;
        if(inputs.SouthBtn) _maintainedTime += deltaTime;
        if(inputs.SouthBtnUp) _maintainedTime = 0;
        
        // just maintain A, always success, no failure on which button done
        // Debug.Log($"Maintain is ticking : {_totalTickTime}/{ErrorRatio} , {_maintainedTime}/{MaintainRatio}");
        
        _itemUIActionView.SetTotalRatio(ErrorRatio);
        _itemUIActionView.SetActionRatio(MaintainRatio);
        
    }
}
