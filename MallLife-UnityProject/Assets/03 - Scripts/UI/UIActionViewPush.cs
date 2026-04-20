using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIActionViewPush : MonoBehaviour, IUIActionView
{
    [Header("References")]
    [SerializeField] private UIDocument _document;

    
    private VisualElement _rootUI;
    
    private VisualElement _activePanel;
    private VisualElement _successPanel;
    private VisualElement _failedPanel;
    private VisualElement _progressBar;
    
    private Label _title;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rootUI = _document.rootVisualElement.Q<VisualElement>("Root");

        _activePanel = _document.rootVisualElement.Q<VisualElement>("ActivePanel");
        _successPanel = _document.rootVisualElement.Q<VisualElement>("SuccesPanel");
        _failedPanel = _document.rootVisualElement.Q<VisualElement>("FailedPanel");
        
        _progressBar = _document.rootVisualElement.Q<VisualElement>("ProgressBar");
        
        _title = _document.rootVisualElement.Q<Label>("Title");

        if (_rootUI == null) Debug.LogWarning("Root UI Not Found");
        
    }
    private void Start()
    {
        Hide();
        SetStartPanel();
    }

    // Update is called once per frame

    public void Show()
    {
        if (_rootUI != null)
            _rootUI.style.display = DisplayStyle.Flex;
    }
    public void Hide()
    {
        if (_rootUI != null)
            _rootUI.style.display = DisplayStyle.None;
    }

    public void SetStartPanel()
    {
        _activePanel.style.display = DisplayStyle.Flex;
        _successPanel.style.display = DisplayStyle.None;
        _failedPanel.style.display = DisplayStyle.None;
    }
    public void SetActivePanel()
    {
        _activePanel.style.display = DisplayStyle.Flex;
        _successPanel.style.display = DisplayStyle.None;
        _failedPanel.style.display = DisplayStyle.None;
    }
    public void SetSuccessPanel()
    {
        _activePanel.style.display = DisplayStyle.None;
        _successPanel.style.display = DisplayStyle.Flex;
        _failedPanel.style.display = DisplayStyle.None;
    }
    public void SetYellowSuccessPanel()
    {
        throw new System.NotImplementedException();
    }
    public void SetFailedPanel()
    {
        _activePanel.style.display = DisplayStyle.None;
        _successPanel.style.display = DisplayStyle.None;
        _failedPanel.style.display = DisplayStyle.Flex;
    }

    public void SetErrorRatio(float ratio)
    {
        _progressBar.style.width = new Length(100 * (1 - ratio), LengthUnit.Percent);
    }
    public void SetTitle(string title)
    {
        if (_title != null) _title.text = title;
    }
}
