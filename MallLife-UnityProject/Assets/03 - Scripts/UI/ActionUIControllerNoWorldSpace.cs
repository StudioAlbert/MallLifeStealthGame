using System;
using UnityEngine;
using UnityEngine.UIElements;

[Obsolete("Code for memory use, UI Placement", true)]
public class ActionUIControllerNoWorldSpace : MonoBehaviour, IUIActionView
{
    [Header("References")]
    [SerializeField] private UIDocument _document;

    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private bool _centered = false;

    private Camera _mainCamera;
    private VisualElement _rootUI;
    private VisualElement _panel;
    private VisualElement _startPanel;
    private VisualElement _activePanel;
    private VisualElement _successPanel;
    private VisualElement _failedPanel;
    private VisualElement _progressBar;
    private VisualElement _maintainBar;

    public Transform ObjectToFollow
    {
        get => _objectToFollow;
        set => _objectToFollow = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rootUI = _document.rootVisualElement.Q<VisualElement>("Root");
        _panel = _document.rootVisualElement.Q<VisualElement>("Panel");

        _startPanel = _document.rootVisualElement.Q<VisualElement>("StartPanel");
        _activePanel = _document.rootVisualElement.Q<VisualElement>("ActivePanel");
        _successPanel = _document.rootVisualElement.Q<VisualElement>("SuccesPanel");
        _failedPanel = _document.rootVisualElement.Q<VisualElement>("FailedPanel");
        
        _progressBar = _document.rootVisualElement.Q<VisualElement>("ProgressBar");
        _maintainBar = _document.rootVisualElement.Q<VisualElement>("MaintainBar");

        if (_rootUI == null) Debug.LogWarning("Root UI Not Found");
        if (_panel == null) Debug.LogWarning("Panel Not Found");
        
    }
    private void Start()
    {
        _mainCamera = Camera.main;
        
        Hide();
        SetStartPanel();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        var containerLayoutSize = _rootUI.layout.size;
        var panelSize = _panel.layout.size;
        Debug.Log($"Container size {containerLayoutSize}");

        if (_objectToFollow)
        {
            Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(_objectToFollow.position);

            var cameraSpaceLocation = new Vector3(viewportPoint.x * containerLayoutSize.x,
                (1 - viewportPoint.y) * containerLayoutSize.y,
                viewportPoint.z);
            if (_centered)
                cameraSpaceLocation -= new Vector3(panelSize.x / 2, panelSize.y / 2, 0);

            _panel.style.translate = new Translate(cameraSpaceLocation.x, cameraSpaceLocation.y, 0);
        }

    }

    public void Show()
    {
        if (_rootUI != null) _rootUI.style.display = DisplayStyle.Flex;
        // _uiDocument.enabled = true;
        // _itemUIController.gameObject.SetActive(true);
    }
    public void Hide()
    {
        if (_rootUI != null) _rootUI.style.display = DisplayStyle.None;
        // _uiDocument.enabled = false;
        // _itemUIController.gameObject.SetActive(false);
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
    public void SetYellowSuccessPanel()
    {
        throw new System.NotImplementedException();
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
        _progressBar.style.width = new Length(100 * ratio, LengthUnit.Percent);
    }
    public void SetMaintainRatio(float ratio)
    {
        _maintainBar.style.width = new Length(100 * ratio, LengthUnit.Percent);
    }
}
