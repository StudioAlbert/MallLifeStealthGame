using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActionViewMovingBall : BasicView
{
    private VisualElement _totalTimeBar;
    private VisualElement _actionBar;
    private VisualElement _movingBall;
    private Label _title;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _totalTimeBar = _document.rootVisualElement.Q<VisualElement>("TotalTimeBar");
        _actionBar = _document.rootVisualElement.Q<VisualElement>("ActionBar");
        _movingBall = _document.rootVisualElement.Q<VisualElement>("MovingBall");
        _title = _document.rootVisualElement.Q<Label>("Title");
        
        Hide();
    }
    
    public void SetErrorRatio(float ratio)
    {
        if (_totalTimeBar == null) return;
        _totalTimeBar.style.width = new Length(100 * (1 - ratio), LengthUnit.Percent);
    }
    public void SetMovingCursor(float ratio)
    {
        if (_movingBall == null) return;
        _movingBall.style.translate = new Translate(_actionBar.layout.size.x * ratio, 0, 0);

        Debug.Log("Is it moving  ? : " + _movingBall.style.translate);
    }
    public void SetTitle(string title)
    {
        if (_title != null) _title.text = title;
    }

}
