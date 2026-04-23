using UnityEngine;

public abstract class ActionsDescriptorSO : ScriptableObject
{
    public float TotalTime = 25f;
    public float ReliveTime = 5f;
}

[CreateAssetMenu(fileName = "New Maintain Descriptor", menuName = "Mall Life/Maintain Descriptor")]
public class MaintainDescriptor : ActionsDescriptorSO
{
    public float ActionTime = 5f;
}

[CreateAssetMenu(fileName = "New Moving Ball Descriptor", menuName = "Mall Life/Moving Ball Descriptor")]
public class MovingBallDescriptor : ActionsDescriptorSO
{
    public float MovingSpeed = 2f;
    public float GreenZoneSize = 0.15f;
    public float YellowZoneSize = 0.35f;
    
}

[CreateAssetMenu(fileName = "New Simple Descriptor", menuName = "Mall Life/Simple Descriptor")]
public class SimplePushDescriptor : ActionsDescriptorSO
{
}