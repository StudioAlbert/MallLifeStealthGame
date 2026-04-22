using System;
using UnityEngine;

public class QTEHandlerMovingBall : MonoBehaviour, IQTEHandler
{
    [Header("References")]
    [SerializeField] private UIActionViewMovingBall _itemUIActionView;
    
    [Header("UI Behaviour")]
    [SerializeField] private bool _needToConfirm;
    [SerializeField] private bool _autoClose;
    [SerializeField] private AnimationCurve _actionCurve;
    
    public IUIQteView UIActionView => _itemUIActionView;
    public bool NeedToConfirm => _needToConfirm;
    public bool AutoClose => _autoClose;
    
    private Action<ActionResult> _handlerDone;
    
    private float _totalTickTime;
    private MovingBallDescriptor _descriptor;

    // Fix this with UI, here is some range placeholder
    private float ErrorRatio => _totalTickTime / _descriptor.TotalTime;
    // private float MovingRatio => Mathf.Repeat(0.5f + (_totalTickTime / _descriptor.MovingSpeed), 1.0f);
    private float MovingRatio => _actionCurve.Evaluate(0.5f + (_totalTickTime / _descriptor.MovingSpeed));

    public void Init(GameObject actionObject, Action<ActionResult> handlerDone)
    {
        _handlerDone = handlerDone;
        _totalTickTime = 0;
        
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIActionView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as MovingBallDescriptor;
        }
    }


    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if (_totalTickTime >= _descriptor.TotalTime)
            _handlerDone?.Invoke(ActionResult.Failed);
        
        if(inputs.SouthBtnUp)
            _handlerDone?.Invoke(Resolve());

        _totalTickTime += deltaTime;

        // just maintain A, always success, no failure on which button done
        // Debug.Log($"Moving Ball is ticking : {_totalTickTime}/{ErrorRatio} , {_totalTickTime}/{MovingRatio}");

        _itemUIActionView.SetErrorRatio(ErrorRatio);
        _itemUIActionView.SetMovingCursor(MovingRatio);

    }

    private ActionResult Resolve()
    {
        // Green zone, Success
        if (MovingRatio >= 0.5f * (1 - _descriptor.GreenZoneSize) && MovingRatio <= 0.5f * (1 + _descriptor.GreenZoneSize))
        {
            return ActionResult.Success;
        }
        
        // Yellow zone, Success
        if (MovingRatio >= 0.5f * (1 - _descriptor.YellowZoneSize) && MovingRatio <= 0.5f * (1 + _descriptor.YellowZoneSize))
        {
            return ActionResult.MidTierResult;
        }
        
        // Red Zone, failed
        return ActionResult.Failed;

    }
}
