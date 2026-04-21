public class ActionStateInactive : ActionState
{

    public ActionStateInactive(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        _actionHandler?.UIActionView.Hide();
    }
    
}
