
using System;
public class QTEStateFailure : QTEState
{
    public QTEStateFailure(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler?.UIView.SetFailedPanel();
    }
    public override void OnExit()
    {
        base.OnExit();
        
        if(_actionHandler == null) return;
        _actionHandler.UIView.Hide();
    }
    
}
