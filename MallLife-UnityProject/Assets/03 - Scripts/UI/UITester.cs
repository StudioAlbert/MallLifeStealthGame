using UnityEngine;
using UnityEngine.UIElements;

public class UITester : MonoBehaviour
{
    private UIDocument _document;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _document = GetComponent<UIDocument>();

        var raiseBtn = _document.rootVisualElement.Q<Button>("Raise");
        raiseBtn.clicked += () =>
        {
            AlertManager.Instance.RaiseAlert(5);
            Debug.Log("Clicked to raise +5");
        };
        
        var releaseBtn = _document.rootVisualElement.Q<Button>("Release");
        releaseBtn.clicked += () =>
        {
            AlertManager.Instance.ReleaseAlert(2);
            Debug.Log("Clicked to Release -2");
        };
        var resetBtn = _document.rootVisualElement.Q<Button>("Reset");
        resetBtn.clicked += () =>
        {
            AlertManager.Instance.Reset();
            Debug.Log("Clicked to reset Alarms");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
