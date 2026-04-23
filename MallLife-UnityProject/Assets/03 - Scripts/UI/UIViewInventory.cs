using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIViewInventory : MonoBehaviour
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

        for (int idxGrid = 0; idxGrid < _gridSize.x * _gridSize.y; idxGrid++)
        {
            TemplateContainer slot;
            if (idxGrid < _inventoryDatas.StolenItems.Count)
            {
                // Filled slot
                slot = PopulateFilledSlot(_inventoryDatas.StolenItems[idxGrid]);
            }
            else
            {
                // Empty slot
                slot = _emptySlotTemplate.Instantiate();
            }
            slot.AddToClassList("slot-container");
            slot.style.width = new Length(100f / _gridSize.x, LengthUnit.Percent);
            _inventoryGrid.Add(slot);
        }

    }
    private TemplateContainer PopulateFilledSlot(StealableItemSO itemSO)
    {

        // Instantiate one slot per item
        TemplateContainer slot = _slotTemplate.Instantiate();

        // Fill it with your SO data
        var icon = slot.Q<VisualElement>("icon");
        icon.style.backgroundImage = itemSO != null ? new StyleBackground(itemSO.Icon) : StyleKeyword.None;

        var labelName = slot.Q<Label>("label-name");
        labelName.text = itemSO != null ? itemSO.Name : "";
        
        var labelValue = slot.Q<Label>("label-value");
        labelValue.text = itemSO != null ? itemSO.NumericValue.ToString() : "";

        return slot;
    }

}
