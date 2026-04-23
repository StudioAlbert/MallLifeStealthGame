using System;
using System.Collections.Generic;
using System.Linq;
using Factions;
using Unity.Properties;

[Serializable]
public class Quest
{

    [CreateProperty] public Faction Faction;
    [CreateProperty] public List<StealableItemSO> ItemsToSteal;

    public bool Complete => ItemsToSteal.Count <= 0; 
    
    public Quest(QuestContent content)
    {
        Faction = content.Faction;
        ItemsToSteal = new List<StealableItemSO>(content._itemsToSteal);
    }

    public bool Collect(InventorySO inventorySo)
    {
        foreach (StealableItemSO stealableItem in ItemsToSteal)
        {
            var goodItem = inventorySo.StolenItems.First(si => si.GetType() == stealableItem.GetType());
            if (goodItem)
            {
                ItemsToSteal.Remove(stealableItem);
                return true;
            }
        }
        
        return false;
        
    }

}
