using System;

public abstract class QTEState : Core.IState
{

    protected IQTEHandler _actionHandler;
    protected readonly CoreInputs.QuickTimeEvents _inputQuickTimeEvents;

    public event Action Exited;
    public event Action Entered;

    public IQTEHandler ActionHandler
    {
        set => _actionHandler = value;
    }

    protected QTEState(CoreInputs.QuickTimeEvents inputQuickTimeEvents)
    {
        _inputQuickTimeEvents = inputQuickTimeEvents;
    }

    public virtual void OnEnter() => Entered?.Invoke();
    public virtual void OnExit() => Exited?.Invoke();
    public virtual void Tick(float deltaTime){}
    
}