public class ActionStateInactive : ActionState
{

    public ActionStateInactive(Inputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        _actionHandler?.UIActionView.Hide();
    }
    
}
