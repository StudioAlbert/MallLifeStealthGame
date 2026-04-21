public class ActionStateSuccess : ActionState
{

    public ActionStateSuccess(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        _actionHandler?.UIActionView.ShowPanel("SuccessPanel");
    }
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler?.UIActionView.Hide();
    }
}
