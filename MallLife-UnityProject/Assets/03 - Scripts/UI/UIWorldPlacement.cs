using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIWorldPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument _document;
    
    [Header("Placement")]
    [SerializeField] private bool _centered;
    [SerializeField] private Transform _toFollow;

    private Camera _mainCamera;
    private VisualElement _rootUI;
    private VisualElement _panel;


    public Transform ToFollow
    {
        get => _toFollow;
        set => _toFollow = value;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rootUI = _document.rootVisualElement.Q<VisualElement>("Root");
        _panel = _document.rootVisualElement.Q<VisualElement>("Panel");
    }

    private void LateUpdate()
    {
        SetToWorldPosition();
    }

    private void SetToWorldPosition()
    {

        var containerLayoutSize = _rootUI.layout.size;
        var panelSize = _panel.layout.size;
        Debug.Log($"Container size {containerLayoutSize}");

        if (_toFollow)
        {
            Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(_toFollow.position);

            var cameraSpaceLocation = new Vector3(viewportPoint.x * containerLayoutSize.x,
                (1 - viewportPoint.y) * containerLayoutSize.y,
                viewportPoint.z);
            if (_centered)
                cameraSpaceLocation -= new Vector3(panelSize.x / 2, panelSize.y / 2, 0);

            _panel.style.translate = new Translate(cameraSpaceLocation.x, cameraSpaceLocation.y, 0);
        }
    }
}
