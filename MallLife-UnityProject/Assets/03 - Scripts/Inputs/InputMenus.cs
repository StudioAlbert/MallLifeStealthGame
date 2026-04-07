using Inputs;
using UnityEngine;

public class InputMenus : MonoBehaviour
{

    private GameControls _controls;
    
    // Menus -----------------------------------
    public bool Inventory;
    public bool Objectives;
    public bool StealthView;

    private bool _inventoryUp;
    private bool _inventoryDown;

    public bool InventoryUp => Utils.OneUseValue(ref _inventoryUp);
    public bool InventoryDown => Utils.OneUseValue(ref _inventoryDown);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _controls = new GameControls();
        
        _controls.Menus.MenuEquipment.started += _ =>
        {
            _inventoryUp = true;
            Inventory = true;
            _inventoryDown = false;
        };
        _controls.Menus.MenuEquipment.canceled += _ =>
        {
            _inventoryUp = false;
            Inventory = false;
            _inventoryDown = false;
        };

        _controls.Menus.MenuObjectives.started += _ => Objectives = true;
        _controls.Menus.MenuObjectives.canceled += _ => Objectives = false;

        _controls.Menus.MenuStealthView.started += _ => StealthView = true;
        _controls.Menus.MenuStealthView.canceled += _ => StealthView = false;

    }

    private void OnEnable() => _controls.Menus.Enable();
    private void OnDisable() => _controls.Menus.Disable();
    private void OnDestroy() => _controls.Dispose();
    
}
