public class ActionStateStart : ActionState
{

    public ActionStateStart(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler.UIActionView.Show();
        _actionHandler.UIActionView.SetStartPanel();
    }
    
}
