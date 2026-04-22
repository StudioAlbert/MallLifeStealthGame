using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class UIAlarmView : MonoBehaviour
{

    private UIDocument _uiDocument;

    private Label stateLabel;
    private VisualElement stateFrame;
    private VisualElement alertBarFill;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupDocument();
    }
    private void SetupDocument()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (!_uiDocument)
        {
            Debug.LogWarning("No alert document.");
            return;
        }

        _uiDocument.rootVisualElement.dataSource = AlertManager.Instance;
        
        // Bind to element
        stateLabel = _uiDocument.rootVisualElement.Q<Label>("State");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
