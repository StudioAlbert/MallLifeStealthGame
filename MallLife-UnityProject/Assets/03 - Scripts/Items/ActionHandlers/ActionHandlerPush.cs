using System;
using UnityEngine;

public class ActionHandlerPush
    : MonoBehaviour, IActionHandler 
{
    [Header("References")]
    [SerializeField] private UIActionViewPush _itemUIActionView;
    [SerializeField] private UIWorldPlacement _worldPlacement;

    
    private float _totalTickTime;
    private Action<bool> _onComplete;
    private SimplePushDescriptor _descriptor;
    
    public IUIActionView UIActionView => _itemUIActionView;
    
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
            _itemUIActionView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as SimplePushDescriptor;
        }
    }
    
    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if(inputs.SouthBtnDown)
            _onComplete?.Invoke(true);
        
        if(_totalTickTime >= _descriptor.FailTime)
            _onComplete?.Invoke(false);
        
        _totalTickTime += deltaTime;
        
        // just maintain A, always success, no failure on which button done
        Debug.Log($"Waiting a push : {_totalTickTime}/{ErrorRatio}");
        
        _itemUIActionView.SetErrorRatio(ErrorRatio);
        
    }
    
}
