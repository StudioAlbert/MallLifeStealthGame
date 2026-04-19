using UnityEngine;

[CreateAssetMenu(fileName = "New Moving Ball Descriptor", menuName = "Mall Life/Moving Ball Descriptor")]
public class MovingBallDescriptor : ActionDescriptor
{
    public float MovingSpeed = 2f;
    public float GreenZoneSize = 0.15f;
    public float YellowZoneSize = 0.35f;


}
