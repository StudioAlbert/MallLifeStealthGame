public class QTEStateSuccess : QTEState
{

    public QTEStateSuccess(Inputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_actionHandler == null) return;
        _actionHandler.UIView.SetSuccessPanel();
    }
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler.UIView.Hide();
    }
}
