public class ActionStateStart : ActionState
{

    public ActionStateStart(Inputs.QuickTimeEvents inputQuickTimeEvents) : base(inputQuickTimeEvents) {}
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnEnter()
    {
        base.OnEnter();
        
        _actionHandler?.UIActionView.Show();
        _actionHandler?.UIActionView.ShowPanel("StartPanel");
        _actionHandler?.UIActionView.HidePanel("SuccessPanel");
        _actionHandler?.UIActionView.HidePanel("FailedPanel");
        _actionHandler?.UIActionView.HidePanel("YellowSuccessPanel");
        _actionHandler?.UIActionView.HidePanel("ActivePanel");
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler?.UIActionView.HidePanel("StartPanel");
    }

}
