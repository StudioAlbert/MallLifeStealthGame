public interface IUIActionView
{
    public void Show();
    public void Hide();

    public void SetStartPanel();
    public void SetActivePanel();
    public void SetSuccessPanel();
    public void SetFailedPanel();

    public void SetYellowSuccessPanel();
}

public interface IUIQteView
{
    public void SetYellowSuccessPanel();
}
