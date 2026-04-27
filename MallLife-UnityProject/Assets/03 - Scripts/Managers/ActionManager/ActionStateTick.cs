using Unity.VisualScripting;
public class ActionStateTick : ActionState
{

    public ActionStateTick(Inputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) { }

    public override void OnEnter()
    {
        base.OnEnter();

        // Depends on future Action Handler Factory 
        _inputQuickTimeEvents.ResetInputs();

        _actionHandler?.UIActionView.Show();
        _actionHandler?.UIActionView.ShowPanel("ActivePanel");
        _actionHandler?.UIActionView.HidePanel("SuccessPanel");
        _actionHandler?.UIActionView.HidePanel("FailedPanel");
        _actionHandler?.UIActionView.HidePanel("YellowSuccessPanel");
    }
    public override void OnExit()
    {
        base.OnExit();
        
        _inputQuickTimeEvents.ResetInputs();
        _actionHandler?.UIActionView.HidePanel("ActivePanel");
    }
    public override void Tick(float deltaTime)
    {
        _actionHandler?.Tick(deltaTime, _inputQuickTimeEvents);
    }
}
