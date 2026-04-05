using System;
using UnityEngine;

public class QTEHandlerPush
    : MonoBehaviour, IQTEHandler 
{
    [Header("References")]
    [SerializeField] private UIViewPush _itemUIView;
    [SerializeField] private UIWorldPlacement _worldPlacement;
    
    [Header("UI Settings")]
    [SerializeField] private bool _needToConfirm = false;
    
    private float _totalTickTime;
    private Action<bool> _onComplete;
    private SimplePushDescriptor _descriptor;
    
    public IUIView UIView => _itemUIView;
    public bool NeedToConfirm => _needToConfirm;
    
    // Fix this with UI, here is some range placeholder
    public float ErrorRatio => _totalTickTime / _descriptor.FailTime;
    
    public void Init(GameObject actionObject, Action<bool> onComplete)
    {
        
        _totalTickTime = 0;
        _onComplete = onComplete;
        _worldPlacement.ToFollow = actionObject.transform;
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as SimplePushDescriptor;
        }
    }
    
    public void Tick(float deltaTime, Inputs.QuickTimeEvents inputs)
    {
        if(inputs.SouthBtnDown)
            _onComplete?.Invoke(true);
        
        if(_totalTickTime >= _descriptor.FailTime)
            _onComplete?.Invoke(false);
        
        _totalTickTime += deltaTime;
        
        // just maintain A, always success, no failure on which button done
        Debug.Log($"Waiting a push : {_totalTickTime}/{ErrorRatio}");
        
        _itemUIView.SetErrorRatio(ErrorRatio);
        
    }
    
}
