public class ActionStateShowPanel : ActionState
{
    public ActionStateShowPanel(Inputs.QuickTimeEvents inputQuickTimeEvents, string panel) :
        base(inputQuickTimeEvents)
    {
        _panel = panel;
    }
    private readonly string _panel;

    public override void OnEnter()
    {
        base.OnEnter();
        _actionHandler?.UIActionView.ShowPanel(_panel);
    }
    public override void OnExit()
    {
        base.OnExit();
        _actionHandler?.UIActionView.Hide();
    }
}
