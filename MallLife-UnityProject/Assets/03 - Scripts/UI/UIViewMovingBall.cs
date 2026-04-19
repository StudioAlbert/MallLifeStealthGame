using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIViewMovingBall : MonoBehaviour, IUIView
{
    [Header("References")]
    [SerializeField] private UIDocument _document;
    
    private Camera _mainCamera;
    private VisualElement _rootUI;
    private VisualElement _panel;
    private VisualElement _startPanel;
    private VisualElement _activePanel;
    private VisualElement _successPanel;
    private VisualElement _failedPanel;
    private VisualElement _errorBar;
    
    private VisualElement _progressBar;
    private VisualElement _movingBall;
    
    private Label _title;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rootUI = _document.rootVisualElement.Q<VisualElement>("Root");

        _startPanel = _document.rootVisualElement.Q<VisualElement>("StartPanel");
        _activePanel = _document.rootVisualElement.Q<VisualElement>("ActivePanel");
        _successPanel = _document.rootVisualElement.Q<VisualElement>("SuccesPanel");
        _failedPanel = _document.rootVisualElement.Q<VisualElement>("FailedPanel");
        
        _errorBar = _document.rootVisualElement.Q<VisualElement>("ErrorBar");
        _progressBar = _document.rootVisualElement.Q<VisualElement>("ProgressBar");
        _movingBall = _document.rootVisualElement.Q<VisualElement>("MovingBall");

        _title = _document.rootVisualElement.Q<Label>("Title");

        if (_rootUI == null) Debug.LogWarning("Root UI Not Found");
        if (_panel == null) Debug.LogWarning("Panel Not Found");
        
    }
    private void Start()
    {
       Hide();
        SetStartPanel();
    }
    public void Show()
    {
        if (_rootUI != null) _rootUI.style.display = DisplayStyle.Flex;
    }
    public void Hide()
    {
        if (_rootUI != null) _rootUI.style.display = DisplayStyle.None;
    }

    public void SetStartPanel()
    {
        _startPanel.style.display = DisplayStyle.Flex;
        _activePanel.style.display = DisplayStyle.None;
        _successPanel.style.display = DisplayStyle.None;
        _failedPanel.style.display = DisplayStyle.None;
    }
    public void SetActivePanel()
    {
        _startPanel.style.display = DisplayStyle.None;
        _activePanel.style.display = DisplayStyle.Flex;
        _successPanel.style.display = DisplayStyle.None;
        _failedPanel.style.display = DisplayStyle.None;
    }
    public void SetSuccessPanel()
    {
        _startPanel.style.display = DisplayStyle.None;
        _activePanel.style.display = DisplayStyle.None;
        _successPanel.style.display = DisplayStyle.Flex;
        _failedPanel.style.display = DisplayStyle.None;
    }
    public void SetFailedPanel()
    {
        _startPanel.style.display = DisplayStyle.None;
        _activePanel.style.display = DisplayStyle.None;
        _successPanel.style.display = DisplayStyle.None;
        _failedPanel.style.display = DisplayStyle.Flex;
    }

    public void SetErrorRatio(float ratio)
    {
        if(_errorBar == null) return;
        _errorBar.style.width = new Length(100 * (1 - ratio), LengthUnit.Percent);
    }
    public void SetMovingCursor(float ratio)
    {
        if(_movingBall == null) return;
        _movingBall.style.translate = new Translate(_progressBar.layout.size.x * ratio, 0, 0);
        
        Debug.Log("Is it moving  ? : " + _movingBall.style.translate);
    }
    public void SetTitle(string title)
    {
        if (_title != null) _title.text = title;
    }
}
