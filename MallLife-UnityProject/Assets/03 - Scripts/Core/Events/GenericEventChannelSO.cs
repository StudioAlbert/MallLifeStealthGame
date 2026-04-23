using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannelSO<T> : ScriptableObject
{
    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T parameter)
    {
        if (OnEventRaised == null)
            return;

        OnEventRaised.Invoke(parameter);
    }
}
public abstract class GenericEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised == null)
            return;

        OnEventRaised.Invoke();
    }
}

[CreateAssetMenu(menuName = "Events/Float EventChannel", fileName = "FloatEventChannel")]
public class EventChannelFloatSO : GenericEventChannelSO<float> {}

[CreateAssetMenu(menuName = "Events/Int EventChannel", fileName = "IntEventChannel")]
public class EventChannelIntSO : GenericEventChannelSO<int> {}

[CreateAssetMenu(menuName = "Events/Void EventChannel", fileName = "VoidEventChannel")]
public class EventChannelVoidSO : GenericEventChannelSO {}