public class ActionStateSuccess : ActionState
{

    public ActionStateSuccess(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler.UIActionView.SetSuccessPanel();
    }
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler.UIActionView.Hide();
    }
}
