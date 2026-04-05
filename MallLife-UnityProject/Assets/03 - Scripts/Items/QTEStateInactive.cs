public class QTEStateInactive : QTEState
{

    public QTEStateInactive(Inputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler?.UIView.Hide();
    }
    
}
