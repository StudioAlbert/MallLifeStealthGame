using Unity.VisualScripting;
public class ActionStateTick : ActionState
{

    public ActionStateTick(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) { }

    public override void OnEnter()
    {
        base.OnEnter();

        // Depends on future Action Handler Factory 
        _inputQuickTimeEvents.ResetInputs();

        if (_actionHandler == null) return;
        _actionHandler.UIActionView.Show();
        _actionHandler.UIActionView.SetActivePanel();
    }
    public override void OnExit()
    {
        base.OnExit();
        
        _inputQuickTimeEvents.ResetInputs();
    }
    public override void Tick(float deltaTime)
    {
        if (_actionHandler == null) return;
        _actionHandler.Tick(deltaTime, _inputQuickTimeEvents);
    }
}
