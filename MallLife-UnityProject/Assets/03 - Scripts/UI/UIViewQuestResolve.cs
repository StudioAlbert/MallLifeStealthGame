using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class UIViewQuestResolve : MonoBehaviour, IUIMenuView
{

    [Header("References")]
    [SerializeField] private UIDocument _questResolveDocument;
    [SerializeField] private InventorySO _inventoryDatas;
    [SerializeField] VisualTreeAsset _slotTemplate; // drag InventorySlot.uxml here
    [SerializeField] VisualTreeAsset _emptySlotTemplate; // drag InventorySlot.uxml here

    [Header("Settings")]
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(2, 2);
    
    public event Action OnExit;
    public event Action OnConfirm;
    
    private VisualElement _root;
    private Button _exit;
    private Button _confirm;
    private VisualElement _inventoryGrid;

    private float _button;
    private bool _isActive;

    public void OnEnable()
    {
        if(_questResolveDocument)
        {
            _root = _questResolveDocument.rootVisualElement;

            _exit = _root.Q<Button>("cancel-btn");
            _confirm = _root.Q<Button>("close-deal-btn");
            _inventoryGrid = _root.Q<VisualElement>("inventory-grid");
            
        }
    }

    public void Show()
    { 
        _root.style.display = DisplayStyle.Flex;
        
        // Populate at start
        UISubViewInventoryGrid.PopulateGrid(
            _inventoryGrid,
            _inventoryDatas,
            _slotTemplate,
            _emptySlotTemplate,
            _gridSize
        );
        
        BindElements();
    }
    public void Hide()
    {
        _root.style.display = DisplayStyle.None;
        UnbindElements();
    }
    public bool IsActive => (_root.style.display == DisplayStyle.Flex);
    
    private void BindElements()
    {
        if (_exit != null) _exit.clicked += OnExit;
        if (_confirm != null) _confirm.clicked += OnConfirm; 
    }
    private void UnbindElements()
    {
        if (_exit != null) _exit.clicked -= OnExit;
        if (_confirm != null) _confirm.clicked -= OnConfirm;
    }
    
}