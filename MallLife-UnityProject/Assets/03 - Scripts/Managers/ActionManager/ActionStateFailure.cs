
using System;
public class ActionStateFailure : ActionState
{
    public ActionStateFailure(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler?.UIActionView.SetFailedPanel();
    }
    public override void OnExit()
    {
        base.OnExit();
        
        if(_actionHandler == null) return;
        _actionHandler.UIActionView.Hide();
    }
    
}
