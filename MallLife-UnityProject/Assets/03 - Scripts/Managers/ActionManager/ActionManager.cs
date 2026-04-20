using System;
using System.Collections;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class ActionManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private CoreInputs.QuickTimeEvents _inputQuickTimeEvents;
    [SerializeField] private ActionHandlerMaintain _actionHandlerMaintain;
    [SerializeField] private ActionHandlerPush _actionHandlerPush;
    
    [Header("UI Behavior Settings")]
    [SerializeField] private float _timeBeforeClosing = 0.75f;
    [SerializeField] private float _timeBeforeUnlock = 2f;
    [SerializeField] private bool _needToConfirm = false;
    [SerializeField] private bool _autoClose = false;

    private readonly Core.StateMachine _actionStateMachine = new Core.StateMachine();
    private ActionStateInactive _inactiveState;
    private ActionStateStart _startState;
    private ActionStateTick _tickState;
    private ActionStateSuccess _successState;
    private ActionStateFailure _failureState;

    public event Action OnSuccess;
    public event Action OnFailure;

    private IActionHandler _actionHandler;

    private float _lastTimeUnlocked;
    private bool _lockStateMachine = false;

    public static ActionManager Instance { get; private set; }

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
        _successState = new ActionStateSuccess(_inputQuickTimeEvents);
        _failureState = new ActionStateFailure(_inputQuickTimeEvents);

        // Wiring events
        _inactiveState.Entered += StopAllCoroutines;
        _successState.Entered += DelayedForceChange;
        _failureState.Entered += DelayedForceChange;
        
        _successState.Exited += HandleSuccess;
        _failureState.Exited += HandleFailure;

        // Transitions
        _actionStateMachine.AddTransition(_startState, _tickState, () => _inputQuickTimeEvents.ValidateUp);
        _actionStateMachine.AddTransition(_startState, _inactiveState, () => _inputQuickTimeEvents.CancelUp);
        _actionStateMachine.AddTransition(_failureState, _inactiveState, () => _inputQuickTimeEvents.CancelUp || _inputQuickTimeEvents.ValidateUp);
        _actionStateMachine.AddTransition(_successState, _inactiveState, () => _inputQuickTimeEvents.CancelUp || _inputQuickTimeEvents.ValidateUp);
        // Start with inactive
        _actionStateMachine.ChangeState(_inactiveState);
    }

    private void OnDisable()
    {
        // UnWiring events
        _inactiveState.Entered -= StopAllCoroutines;
        _successState.Entered -= DelayedForceChange;
        _failureState.Entered -= DelayedForceChange;

        _successState.Exited -= HandleSuccess;
        _failureState.Exited -= HandleFailure;
    }

    private void Update()
    {
        _lockStateMachine = (Time.time - _lastTimeUnlocked <= _timeBeforeUnlock);
        _actionStateMachine.Tick(Time.deltaTime);
    }

    public void StartAction(GameObject objectToFollow)
    {
        
        if(_lockStateMachine) return;
        
        _actionHandler = GetQTE(objectToFollow);

        if (_actionHandler != null)
        {
            _actionHandler.Init(objectToFollow, r => _actionStateMachine.ChangeState(r ? _successState : _failureState));
           
            // Set action handlers ----------------------------------------
            _inactiveState.ActionHandler = _actionHandler;
            _startState.ActionHandler = _actionHandler;
            _tickState.ActionHandler = _actionHandler;
            _successState.ActionHandler = _actionHandler;
            _failureState.ActionHandler = _actionHandler;

            _actionStateMachine.ChangeState(_needToConfirm ? _startState : _tickState);

        }
    }

    // QTE Factory ----------------------------------------------------------------------
    private IActionHandler GetQTE(GameObject QTEObject)
    {
        IActionHandler handlerResult;

        if (!QTEObject.TryGetComponent(out Stealable stealable))
            return null;

        // Return one object depending on
        // - Type
        // - Activation
        return stealable.Descriptor switch
        {
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

    private void HandleSuccess()
    {
        OnSuccess?.Invoke();
        _lastTimeUnlocked = Time.time;
    }
    private void HandleFailure()
    {
        OnFailure?.Invoke();
        _lastTimeUnlocked = Time.time;
    }
    private void DelayedForceChange()
    {
        if(_autoClose)
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
