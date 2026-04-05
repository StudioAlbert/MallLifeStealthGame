using System;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[Obsolete("Code for memory use, UI Placement", true)]
public class ActionUIControllerWorldSpace : MonoBehaviour
{
    private Camera _mainCamera;
    private UIDocument _document;
    private VisualElement _panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _mainCamera = Camera.main;
        _document = GetComponent<UIDocument>();
        if (_document)
        {
            _panel = _document.rootVisualElement.Q<VisualElement>("Panel");
            if(_panel == null) Debug.LogWarning("Panel Not Found");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Billboarding();
    }

    private void Billboarding()
    {
        Vector3 direction = transform.position - _mainCamera.transform.position;
        // Stay up
        //direction.y = 0;
        // Don't billboard if too close to camera
        if (direction.sqrMagnitude < 0.0001f) return;
        // Face to the camera
        transform.rotation = Quaternion.LookRotation(direction);

    }
}
