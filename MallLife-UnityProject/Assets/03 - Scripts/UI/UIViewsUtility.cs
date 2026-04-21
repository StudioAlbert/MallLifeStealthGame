
    using UnityEngine;
    using UnityEngine.UIElements;
    
    public static class UIViewsUtility
    {
        public static void SetPanelVisibility(UIDocument document, string panelName, bool showPanel)
        {
            VisualElement panel = document.rootVisualElement.Q<VisualElement>(panelName);

            if (panel != null)
                panel.style.display = showPanel ? DisplayStyle.Flex : DisplayStyle.None;
            else
                Debug.LogWarning($"{panelName} does not exist in this ui frame");

        }
    }
