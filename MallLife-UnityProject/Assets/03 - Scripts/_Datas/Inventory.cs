using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Mall Life/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public List<StealableItem> _stolenItems;
    [CreateProperty] public List<StealableItem> StolenItems => _stolenItems;

    public void AddItem(StealableItem item)
    {
        Debug.Log($"Adding item {item.name}");
        _stolenItems.Add(item);
    }
}
