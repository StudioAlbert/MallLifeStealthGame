using UnityEngine;
using UnityEngine.UIElements;

public static class UISubViewInventoryGrid
{
    public static void PopulateGrid(VisualElement grid,
        InventorySO inventoryDatas,
        VisualTreeAsset slotTemplate,
        VisualTreeAsset emptySlotTemplate,
        Vector2Int gridSize)
    {

        if (grid == null)
        {
            Debug.LogWarning("no grid in the document");
            return;
        }

        grid.Clear();

        for (int idxGrid = 0; idxGrid < gridSize.x * gridSize.y; idxGrid++)
        {
            TemplateContainer slot;
            if (idxGrid < inventoryDatas.StolenItems.Count)
            {
                // Filled slot
                slot = PopulateFilledSlot(inventoryDatas.StolenItems[idxGrid], slotTemplate);
            }
            else
            {
                // Empty slot
                slot = emptySlotTemplate.Instantiate();
            }
            slot.AddToClassList("slot-container");
            slot.style.width = new Length(100f / gridSize.x, LengthUnit.Percent);
            grid.Add(slot);
        }

    }
    private static TemplateContainer PopulateFilledSlot(StealableItemSO itemSO, VisualTreeAsset slotTemplate)
    {

        // Instantiate one slot per item
        TemplateContainer slot = slotTemplate.Instantiate();

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
