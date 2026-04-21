public class ActionStateYellowSuccess : ActionState
{

    public ActionStateYellowSuccess(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        _actionHandler?.UIActionView.ShowPanel("YellowSuccessPanel");
    }
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler?.UIActionView.Hide();
    }
}
