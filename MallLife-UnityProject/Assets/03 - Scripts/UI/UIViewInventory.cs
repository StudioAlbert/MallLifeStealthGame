using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIViewInventory : MonoBehaviour
{
    
    [SerializeField] UIDocument _inventoryDocument;
    [SerializeField] InventorySO _inventoryDatas;
    [SerializeField] VisualTreeAsset _slotTemplate; // drag InventorySlot.uxml here

    private VisualElement _root;
    private VisualElement _inventoryGrid;
    
    void OnEnable()
    {
        // Get references
        _root = _inventoryDocument.rootVisualElement;
        _inventoryGrid = _root.Q<VisualElement>("inventory-grid");
        // Populate if changes happened
        _inventoryDatas.OnChanged += PopulateGrid;
        // Populate at start
        PopulateGrid();
    }
    private void OnDisable()
    {
        _inventoryDatas.OnChanged -= PopulateGrid;
    }

    private void PopulateGrid()
    {

        if (_inventoryGrid == null)
        {
            Debug.LogWarning("no grid in the document");
            return;
        }
        
        _inventoryGrid.Clear();

        foreach (var item in _inventoryDatas.StolenItems)
        {
            // Instantiate one slot per item
            var slot = _slotTemplate.Instantiate();

            // Fill it with your SO data
            var icon = slot.Q<VisualElement>("icon");
            // icon.style.backgroundImage = item != null
            //     ? new StyleBackground(item.Icon)
            //     : StyleKeyword.None;

            var labelName = slot.Q<Label>("label-name");
            labelName.text = item != null ? item.Name : "";

            var labelValue = slot.Q<Label>("label-value");
            labelValue.text = item != null ? item.NumericValue.ToString() : "";

            _inventoryGrid.Add(slot);
        }
    }

}
