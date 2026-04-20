public class ActionStateYellowSuccess : ActionState
{

    public ActionStateYellowSuccess(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler.UIActionView.SetYellowSuccessPanel();
    }
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler.UIActionView.Hide();
    }
}
