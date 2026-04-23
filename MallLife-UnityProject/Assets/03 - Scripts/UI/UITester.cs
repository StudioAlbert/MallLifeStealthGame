using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UITester : MonoBehaviour
{
    [SerializeField] private EventChannelFloatSO _raiseAlertEvt;
    [SerializeField] private EventChannelFloatSO _releaseAlertEvt;
    [SerializeField] private EventChannelVoidSO _resetAlertEvt;
    
    private UIDocument _document;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _document = GetComponent<UIDocument>();

        BindButton("RawRaise", () => AlertManager.Instance.RaiseAlert(5));
        BindButton("RawRelease", () => AlertManager.Instance.ReleaseAlert(2));
        BindButton("RawReset", () => AlertManager.Instance.Reset());
        
        BindButton("EvtRaise", () => _raiseAlertEvt.RaiseEvent(5));
        BindButton("EvtRelease", () => _releaseAlertEvt.RaiseEvent(1.5f));
        BindButton("EvtReset", () => _resetAlertEvt.RaiseEvent());

    }
    private void BindButton(string btnName, Action function)
    {
        var button = _document.rootVisualElement.Q<Button>(btnName);
        if (button == null)
        {
            Debug.Log("Button name does not fit");
            return;
        }
        
        button.clicked += function;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
