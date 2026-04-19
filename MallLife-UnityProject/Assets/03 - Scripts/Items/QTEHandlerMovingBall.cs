using System;
using UnityEngine;

public class QTEHandlerMovingBall : MonoBehaviour, IQTEHandler
{
    [Header("References")]
    [SerializeField] private UIViewMovingBall _itemUIView;

    [Header("UI Settings")]
    [SerializeField] private bool _needToConfirm = false;

    private float _totalTickTime;
    private Action<bool> _onComplete;
    private MovingBallDescriptor _descriptor;

    public IUIView UIView => _itemUIView;
    public bool NeedToConfirm => _needToConfirm;

    // Fix this with UI, here is some range placeholder
    private float ErrorRatio => _totalTickTime / _descriptor.FailTime;
    private float MovingRatio => Mathf.Repeat(0.5f + (_totalTickTime / _descriptor.MovingSpeed), 1.0f);

    public void Init(GameObject actionObject, Action<bool> onComplete)
    {

        _totalTickTime = 0;
        _onComplete = onComplete;
        
        // Name is the name of a maybe stealable object
        if (actionObject.TryGetComponent(out Stealable stealable))
        {
            _itemUIView.SetTitle($"{stealable.Item.Name} / ${stealable.Item.NumericValue}");
            _descriptor = stealable.Descriptor as MovingBallDescriptor;
        }
    }

    public void Tick(float deltaTime, CoreInputs.QuickTimeEvents inputs)
    {
        if (_totalTickTime >= _descriptor.FailTime)
            _onComplete?.Invoke(false);
        
        if(inputs.SouthBtnUp)
            _onComplete?.Invoke(Resolve());

        _totalTickTime += deltaTime;

        // just maintain A, always success, no failure on which button done
        Debug.Log($"Moving Ball is ticking : {_totalTickTime}/{ErrorRatio} , {_totalTickTime}/{MovingRatio}");

        _itemUIView.SetErrorRatio(ErrorRatio);
        _itemUIView.SetMovingCursor(MovingRatio);

    }

    private bool Resolve()
    {
        // Green zone, Success
        if (MovingRatio >= 0.5f * (1 - _descriptor.GreenZoneSize) && MovingRatio <= 0.5f * (1 + _descriptor.GreenZoneSize))
        {
            return true;
        }
        
        // Yellow zone, Success
        if (MovingRatio >= 0.5f * (1 - _descriptor.YellowZoneSize) && MovingRatio <= 0.5f * (1 + _descriptor.YellowZoneSize))
        {
            return true;
        }
        
        // Red Zone, failed
        return false;

    }
}
