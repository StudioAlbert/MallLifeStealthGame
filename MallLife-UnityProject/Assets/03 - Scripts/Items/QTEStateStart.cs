public class QTEStateStart : QTEState
{

    public QTEStateStart(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler.UIView.Show();
        _actionHandler.UIView.SetStartPanel();
    }
    
}
