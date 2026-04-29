using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIViewInventory : MonoBehaviour, IUIMenuView
{

    [Header("References")]
    [SerializeField] UIDocument _inventoryDocument;
    [SerializeField] InventorySO _inventoryDatas;
    [SerializeField] VisualTreeAsset _slotTemplate; // drag InventorySlot.uxml here
    [SerializeField] VisualTreeAsset _emptySlotTemplate; // drag InventorySlot.uxml here

    [Header("Settings")]
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(2, 2);

    private VisualElement _root;
    private VisualElement _inventoryGrid;
    
    void OnEnable()
    {
        // Get references
        _root = _inventoryDocument.rootVisualElement;
        _inventoryGrid = _root.Q<VisualElement>("inventory-grid");
        // Populate at start
        Populate();
        // Populate if changes happened
        _inventoryDatas.OnChanged += Populate;
        
    }
    private void OnDisable()
    {
        _inventoryDatas.OnChanged -= Populate;
    }
    
    private void Populate() => UISubViewInventoryGrid.PopulateGrid(
        _inventoryGrid,
        _inventoryDatas,
        _slotTemplate,
        _emptySlotTemplate,
        _gridSize);

    public void Show()
    {
        _root.style.display = DisplayStyle.Flex;
    }
    public void Hide()
    {
        _root.style.display = DisplayStyle.None;
    }
    public bool IsActive => (_root.style.display == DisplayStyle.Flex);
}
