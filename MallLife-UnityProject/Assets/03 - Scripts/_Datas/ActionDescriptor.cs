using UnityEngine;

public abstract class ActionDescriptor : ScriptableObject
{
    public float FailTime = 25f;
    public float ReliveTime = 5f;
}