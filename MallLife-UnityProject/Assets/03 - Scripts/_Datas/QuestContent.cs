using System.Collections.Generic;
using Factions;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Content", menuName = "Mall Life/Quest Content")]
public class QuestContent : ScriptableObject
{
    public Faction Faction;
    public List<StealableItem> _itemsToSteal;
    
}
