using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActionViewMaintain : BasicView
{
    private VisualElement _progressBar;
    private VisualElement _maintainBar;
    private Label _title;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _progressBar = _document.rootVisualElement.Q<VisualElement>("ProgressBar");
        _maintainBar = _document.rootVisualElement.Q<VisualElement>("MaintainBar");
        _title = _document.rootVisualElement.Q<Label>("Title");
        
        Hide();
    }
    
    public void SetErrorRatio(float ratio)
    {
        _progressBar.style.width = new Length(100 * (1 - ratio), LengthUnit.Percent);
    }
    public void SetMaintainRatio(float ratio)
    {
        _maintainBar.style.width = new Length(100 * ratio, LengthUnit.Percent);
    }
    public void SetTitle(string title)
    {
        if (_title != null) _title.text = title;
    }
}
