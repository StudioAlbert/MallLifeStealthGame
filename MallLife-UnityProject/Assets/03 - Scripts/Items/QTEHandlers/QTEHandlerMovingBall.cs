using System;
using UnityEngine;

public class QTEHandlerMovingBall : MonoBehaviour, IQTEHandler
{
    [Header("References")]
    [SerializeField] private UIActionViewMovingBall _itemUIActionView;

    private float _totalTickTime;
    private Action<ActionResult> _onComplete;
    private MovingBallDescriptor _descriptor;

    public IUIQteView UIActionView => _itemUIActionView;

    // Fix this with UI, here is some range placeholder
    private float ErrorRatio => _totalTickTime / _descriptor.FailTime;
    private float MovingRatio => Mathf.Repeat(0.5f + (_totalTickTime / _descriptor.MovingSpeed), 1.0f);

    public void Init(GameObject actionObject, Action<bool> onComplete)
    {
        throw new NotImplementedException();
    }
    public void Init(GameObject actionObject, Action<ActionResult> onComplete)
    {

        _totalTickTime = 0;
        _onComplete = onComplete;
        
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIActionView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as MovingBallDescriptor;
        }
    }


    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if (_totalTickTime >= _descriptor.FailTime)
            _onComplete?.Invoke(ActionResult.Failed);
        
        if(inputs.SouthBtnUp)
            _onComplete?.Invoke(Resolve());

        _totalTickTime += deltaTime;

        // just maintain A, always success, no failure on which button done
        Debug.Log($"Moving Ball is ticking : {_totalTickTime}/{ErrorRatio} , {_totalTickTime}/{MovingRatio}");

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
