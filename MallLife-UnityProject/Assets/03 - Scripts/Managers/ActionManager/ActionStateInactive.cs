public class ActionStateInactive : ActionState
{

    public ActionStateInactive(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler?.UIActionView.Hide();
    }
    
}
