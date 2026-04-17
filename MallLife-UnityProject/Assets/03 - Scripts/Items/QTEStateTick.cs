
public class QTEStateTick : QTEState
{

    public QTEStateTick(CoreInputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        // Depends on future Action Handler Factory 
        _inputQuickTimeEvents.ResetInputs();
        
        if(_actionHandler == null) return;
        _actionHandler.UIView.Show();
        _actionHandler.UIView.SetActivePanel();
    }
    public override void Tick(float deltaTime)
    {
        if(_actionHandler == null) return;
        _actionHandler.Tick(deltaTime, _inputQuickTimeEvents);
    }
}
