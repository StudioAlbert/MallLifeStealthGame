using System;
using System.Collections;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class QTEManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private CoreInputs.QuickTimeEvents _inputQuickTimeEvents;
    [SerializeField] private QTEHandlerMaintain _qteHandlerMaintain;
    [SerializeField] private QTEHandlerPush _qteHandlerPush;
    [SerializeField] private QTEHandlerMovingBall _qteHandlerMovingBall;
    
    [Header("UI Behavior Settings")]
    [SerializeField] private float _timeBeforeClosing = 0.75f;
    [SerializeField] private float _timeBeforeUnlock = 2f;
    [SerializeField] private bool _needToConfirm = false;
    [SerializeField] private bool _autoClose = false;

    private readonly Core.StateMachine _actionStateMachine = new Core.StateMachine();
    private QTEStateInactive _inactiveState;
    private QTEStateStart _startState;
    private QTEStateTick _tickState;
    private QTEStateSuccess _successState;
    private QTEStateFailure _failureState;

    public event Action OnSuccess;
    public event Action OnFailure;

    private IQTEHandler _qteHandler;

    private float _lastTimeUnlocked;
    private bool _lockStateMachine = false;

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
        _inactiveState = new QTEStateInactive(_inputQuickTimeEvents);
        _startState = new QTEStateStart(_inputQuickTimeEvents);
        _tickState = new QTEStateTick(_inputQuickTimeEvents);
        _successState = new QTEStateSuccess(_inputQuickTimeEvents);
        _failureState = new QTEStateFailure(_inputQuickTimeEvents);

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

    public void StartQTE(GameObject objectToFollow)
    {
        
        if(_lockStateMachine) return;
        
        _qteHandler = GetQTE(objectToFollow);

        if (_qteHandler != null)
        {
            _qteHandler.Init(objectToFollow, r => _actionStateMachine.ChangeState(r ? _successState : _failureState));
           
            // Set action handlers ----------------------------------------
            _inactiveState.ActionHandler = _qteHandler;
            _startState.ActionHandler = _qteHandler;
            _tickState.ActionHandler = _qteHandler;
            _successState.ActionHandler = _qteHandler;
            _failureState.ActionHandler = _qteHandler;

            _actionStateMachine.ChangeState(_needToConfirm ? _startState : _tickState);

        }
    }

    // QTE Factory ----------------------------------------------------------------------
    private IQTEHandler GetQTE(GameObject QTEObject)
    {
        IQTEHandler handlerResult;

        if (!QTEObject.TryGetComponent(out Stealable stealable))
            return null;

        // Return one object depending on
        // - Type
        // - Activation
        return stealable.Descriptor switch
        {
            MaintainDescriptor => _qteHandlerMaintain.gameObject.activeSelf ? _qteHandlerMaintain : null,
            SimplePushDescriptor => _qteHandlerPush.gameObject.activeSelf ? _qteHandlerPush : null,
            MovingBallDescriptor => _qteHandlerMovingBall.gameObject.activeSelf ? _qteHandlerMovingBall : null,
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
