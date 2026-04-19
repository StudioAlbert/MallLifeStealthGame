using System;
using UnityEngine;

public class QTEHandlerMaintain : MonoBehaviour, IQTEHandler 
{
    [Header("References")]
    [SerializeField] private UIViewMaintain _itemUIView;
    [SerializeField] private UIWorldPlacement _worldPlacement;

    [Header("UI Settings")]
    [SerializeField] private bool _needToConfirm = false;
    
    private float _totalTickTime;
    private float _maintainedTime;
    private Action<bool> _onComplete;
    private MaintainDescriptor _descriptor;
    
    public IUIView UIView => _itemUIView;
    public bool NeedToConfirm => _needToConfirm;
    
    // Fix this with UI, here is some range placeholder
    private float ErrorRatio => _totalTickTime / _descriptor.FailTime;
    private float MaintainRatio => _maintainedTime / _descriptor.SuccessTime;
    
    public void Init(GameObject actionObject, Action<bool> onComplete)
    {
        
        _totalTickTime = 0;
        _maintainedTime = 0;
        _onComplete = onComplete;
        _worldPlacement.ToFollow = actionObject.transform;
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as MaintainDescriptor;
        }
    }
    
    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if(_maintainedTime >= _descriptor.SuccessTime)
            _onComplete?.Invoke(true);
        
        if(_totalTickTime >= _descriptor.FailTime)
            _onComplete?.Invoke(false);
        
        _totalTickTime += deltaTime;
        if(inputs.SouthBtn) _maintainedTime += deltaTime;
        if(inputs.SouthBtnUp) _maintainedTime = 0;
        
        // just maintain A, always success, no failure on which button done
        Debug.Log($"Maintain is ticking : {_totalTickTime}/{ErrorRatio} , {_maintainedTime}/{MaintainRatio}");
        
        _itemUIView.SetErrorRatio(ErrorRatio);
        _itemUIView.SetMaintainRatio(MaintainRatio);
        
    }
}
