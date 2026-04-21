using System;
using UnityEngine;

public class ActionHandlerPush : MonoBehaviour, IQTEHandler 
{
    [Header("References")]
    [SerializeField] private UIActionViewPush _itemUIActionView;
    [SerializeField] private UIWorldPlacement _worldPlacement;

    
    private float _totalTickTime;
    private Action<ActionResult> _onComplete;
    private SimplePushDescriptor _descriptor;
    
    public IUIQteView UIActionView => _itemUIActionView;
    
    // Fix this with UI, here is some range placeholder
    public float ErrorRatio => _totalTickTime / _descriptor.FailTime;
    
    public void Init(GameObject actionObject, Action<ActionResult> onComplete)
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
            _onComplete?.Invoke(ActionResult.Success);
        
        if(_totalTickTime >= _descriptor.FailTime)
            _onComplete?.Invoke(ActionResult.Success);
        
        _totalTickTime += deltaTime;
        
        // just maintain A, always success, no failure on which button done
        Debug.Log($"Waiting a push : {_totalTickTime}/{ErrorRatio}");
        
        _itemUIActionView.SetErrorRatio(ErrorRatio);
        
    }
    
}
