using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActionViewMovingBall : BasicView
{
    private VisualElement _errorBar;
    private VisualElement _progressBar;
    private VisualElement _movingBall;
    private Label _title;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _errorBar = _document.rootVisualElement.Q<VisualElement>("ErrorBar");
        _progressBar = _document.rootVisualElement.Q<VisualElement>("ProgressBar");
        _movingBall = _document.rootVisualElement.Q<VisualElement>("MovingBall");
        _title = _document.rootVisualElement.Q<Label>("Title");
        
        Hide();
    }
    
    public void Show() => UIViewsUtility.SetPanelVisibility(_document, "Root", true);
    public void Hide() => UIViewsUtility.SetPanelVisibility(_document, "Root", false);
    public void ShowPanel(string panelName) => UIViewsUtility.SetPanelVisibility(_document, panelName, true);
    public void HidePanel(string panelName) => UIViewsUtility.SetPanelVisibility(_document, panelName, false);
    
    public void SetErrorRatio(float ratio)
    {
        if (_errorBar == null) return;
        _errorBar.style.width = new Length(100 * (1 - ratio), LengthUnit.Percent);
    }
    public void SetMovingCursor(float ratio)
    {
        if (_movingBall == null) return;
        _movingBall.style.translate = new Translate(_progressBar.layout.size.x * ratio, 0, 0);

        Debug.Log("Is it moving  ? : " + _movingBall.style.translate);
    }
    public void SetTitle(string title)
    {
        if (_title != null) _title.text = title;
    }

}
