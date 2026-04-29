using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Inputs.InputMenus _inputMenus;
    [SerializeField] private UIViewInventory _inventoryView;

    private void OnEnable()
    {
        _inventoryView.gameObject.SetActive(true);
        _inventoryView.Hide();
        _inputMenus.Inventory.OnDown += InventoryShow;
        _inputMenus.Cancel.OnUp += InventoryHide;
    }
    private void OnDisable()
    {
        _inventoryView.gameObject.SetActive(false);
        _inputMenus.Inventory.OnDown -= InventoryShow;
        _inputMenus.Cancel.OnUp -= InventoryHide;
    }
    private void InventoryShow() => _inventoryView?.Show();
    private void InventoryHide() => _inventoryView?.Hide();

}
