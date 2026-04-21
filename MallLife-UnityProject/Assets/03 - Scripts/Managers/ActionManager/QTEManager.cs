using System;
using System.Collections;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class QTEManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private CoreInputs.QuickTimeEvents _inputQuickTimeEvents;
    [SerializeField] private QTEHandlerMovingBall _qteHandlerMovingBall;
    [SerializeField] private ActionHandlerMaintain _actionHandlerMaintain;
    [SerializeField] private ActionHandlerPush _actionHandlerPush;
    
    [Header("UI Behavior Settings")]
    [SerializeField] private float _timeBeforeClosing = 0.75f;
    [SerializeField] private float _timeBeforeUnlock = 2f;

    private readonly Core.StateMachine _actionStateMachine = new Core.StateMachine();
    private ActionStateInactive _inactiveState;
    private ActionStateStart _startState;
    private ActionStateTick _tickState;
    private ActionStateShowPanel _successState;
    private ActionStateShowPanel _yellowSuccessState;
    private ActionStateShowPanel _failureState;

    public event Action<ActionResult> OnResult;

    private IQTEHandler _actionHandler;

    private float _lastTimeUnlocked;
    private bool _lockStateMachine;

    public static QTEManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    private void OnEnable()
    {
        // Init State machine
        // Init states 
        _inactiveState = new ActionStateInactive(_inputQuickTimeEvents);
        _startState = new ActionStateStart(_inputQuickTimeEvents);
        _tickState = new ActionStateTick(_inputQuickTimeEvents);
        _successState = new ActionStateShowPanel(_inputQuickTimeEvents, "SuccessPanel");
        _yellowSuccessState = new ActionStateShowPanel(_inputQuickTimeEvents, "YellowSuccessPanel");
        _failureState = new ActionStateShowPanel(_inputQuickTimeEvents, "FailedPanel");

        // Wiring events
        _inactiveState.Entered += StopAllCoroutines;
        _successState.Entered += DelayedForceChange;
        _yellowSuccessState.Entered += DelayedForceChange;
        _failureState.Entered += DelayedForceChange;
        
        _successState.Exited += HandleSuccess;
        _yellowSuccessState.Exited += HandleYellowSuccess;
        _failureState.Exited += HandleFailure;

        // Transitions
        _actionStateMachine.AddTransition(_startState, _tickState, () => _inputQuickTimeEvents.ValidateUp);
        _actionStateMachine.AddTransition(_startState, _inactiveState, () => _inputQuickTimeEvents.CancelUp);
        _actionStateMachine.AddTransition(_failureState, _inactiveState, () => _inputQuickTimeEvents.CancelUp || _inputQuickTimeEvents.ValidateUp);
        _actionStateMachine.AddTransition(_yellowSuccessState, _inactiveState, () => _inputQuickTimeEvents.CancelUp || _inputQuickTimeEvents.ValidateUp);
        _actionStateMachine.AddTransition(_successState, _inactiveState, () => _inputQuickTimeEvents.CancelUp || _inputQuickTimeEvents.ValidateUp);
        
        // Start with inactive
        _actionStateMachine.ChangeState(_inactiveState);
    }

    private void OnDisable()
    {
        // UnWiring events
        _inactiveState.Entered -= StopAllCoroutines;
        _successState.Entered -= DelayedForceChange;
        _yellowSuccessState.Entered -= DelayedForceChange;
        _failureState.Entered -= DelayedForceChange;

        _successState.Exited -= HandleSuccess;
        _yellowSuccessState.Exited -= HandleYellowSuccess;
        _failureState.Exited -= HandleFailure;
    }

    private void Update()
    {
        _lockStateMachine = (Time.time - _lastTimeUnlocked <= _timeBeforeUnlock);
        _actionStateMachine.Tick(Time.deltaTime);
    }

    public void StartQTE(GameObject objectToFollow)
    {
        // TODO : Lock sytem ?
        //if(_lockStateMachine) return;
        
        // Try to get an actual QTE
        _actionHandler = GetQTE(objectToFollow);
        if (_actionHandler == null) return;
        
        // QTE request succeed
        _actionHandler.Init(objectToFollow, OnComplete);
           
        // Set action handlers ----------------------------------------
        _inactiveState.ActionHandler = _actionHandler;
        _startState.ActionHandler = _actionHandler;
        _tickState.ActionHandler = _actionHandler;
        _successState.ActionHandler = _actionHandler;
        _yellowSuccessState.ActionHandler = _actionHandler;
        _failureState.ActionHandler = _actionHandler;

        _actionStateMachine.ChangeState(_actionHandler.NeedToConfirm ? _startState : _tickState);
    }
    private void OnComplete(ActionResult result)
    { 
        switch(result)
        {
            case ActionResult.Success :
                _actionStateMachine.ChangeState(_successState); 
                break;
            case ActionResult.MidTierResult:
                _actionStateMachine.ChangeState(_yellowSuccessState);
                break;
            case ActionResult.Failed:
                _actionStateMachine.ChangeState(_failureState);
                break;
            default:
                _actionStateMachine.ChangeState(_inactiveState);
                break;
        }
    }

    // QTE Factory ----------------------------------------------------------------------
    private IQTEHandler GetQTE(GameObject QTEObject)
    {
        if (!QTEObject.TryGetComponent(out Stealable stealable))
            return null;

        // Return one object depending on
        // - Type
        // - Activation
        return stealable.Descriptor switch
        {
            MovingBallDescriptor => _qteHandlerMovingBall.gameObject.activeSelf ? _qteHandlerMovingBall : null,
            MaintainDescriptor => _actionHandlerMaintain.gameObject.activeSelf ? _actionHandlerMaintain : null,
            SimplePushDescriptor => _actionHandlerPush.gameObject.activeSelf ? _actionHandlerPush : null,
            _ => null
        };

    }

    public void Interrupt()
    {
        _actionStateMachine.ChangeState(_inactiveState);
        _lockStateMachine = true;
    }

    private void HandleSuccess() => HandleQTEResult(ActionResult.Success);
    private void HandleYellowSuccess() => HandleQTEResult(ActionResult.MidTierResult);
    private void HandleFailure() => HandleQTEResult(ActionResult.Failed);
    private void HandleQTEResult(ActionResult result)
    {
        OnResult?.Invoke(result);
        _lastTimeUnlocked = Time.time;
    }
    
    private void DelayedForceChange()
    {
        if(_actionHandler.AutoClose)
            _actionStateMachine.ChangeState(_inactiveState);
        else
            StartCoroutine(ForceChange());   
    }

    private IEnumerator ForceChange()
    {
        yield return new WaitForSeconds(_timeBeforeClosing);
        _actionStateMachine.ChangeState(_inactiveState);
    }

}
