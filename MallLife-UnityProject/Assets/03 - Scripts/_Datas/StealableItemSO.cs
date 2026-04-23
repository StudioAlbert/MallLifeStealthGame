using UnityEngine;

[CreateAssetMenu(fileName = "New Stealable Item", menuName = "Mall Life/Stealable Item")]
public class StealableItemSO : ScriptableObject
{
    public string Name;
    public int NumericValue;
    public Sprite Icon;
}
