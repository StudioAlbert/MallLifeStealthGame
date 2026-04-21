using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActionViewPush : BasicView
{
    private VisualElement _totalTimeBar;
    private Label _title;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _totalTimeBar = _document.rootVisualElement.Q<VisualElement>("TotalTimeBar");
        _title = _document.rootVisualElement.Q<Label>("Title");
        
        Hide();
    }

   public void SetTotalRatio(float ratio)
    {
        _totalTimeBar.style.width = new Length(100 * (1 - ratio), LengthUnit.Percent);
    }
    public void SetTitle(string title)
    {
        if (_title != null) _title.text = title;
    }
}
