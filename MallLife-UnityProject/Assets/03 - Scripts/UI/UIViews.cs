using UnityEngine;
using UnityEngine.UIElements;

public interface IUIQteView
{

    public void Show();
    public void Hide();

    public void ShowPanel(string panelName);
    public void HidePanel(string panelName);

}

public abstract class BasicView : MonoBehaviour, IUIQteView
{
    [Header("References")]
    [SerializeField] protected UIDocument _document;

    public void Show() => UIViewsUtility.SetPanelVisibility(_document, "Root", true);
    public void Hide() => UIViewsUtility.SetPanelVisibility(_document, "Root", false);
    public void ShowPanel(string panelName) => UIViewsUtility.SetPanelVisibility(_document, panelName, true);
    public void HidePanel(string panelName) => UIViewsUtility.SetPanelVisibility(_document, panelName, false);
    
}
