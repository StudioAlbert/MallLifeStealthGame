using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Mall Life/Inventory", order = 0)]
public class InventorySO : ScriptableObject
{
    
    private List<StealableItemSO> _stolenItems;
    [CreateProperty] public List<StealableItemSO> StolenItems => _stolenItems;

    public event Action OnChanged;

    public void Empty()
    {
        _stolenItems.Clear();
        OnChanged?.Invoke();
    }
    
    public void AddItem(StealableItemSO itemSO)
    {
        Debug.Log($"Adding item {itemSO.name}");
        _stolenItems.Add(itemSO);
        
        OnChanged?.Invoke();
    }
}
