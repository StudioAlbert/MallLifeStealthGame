using UnityEngine;
using UnityEngine.Events;

public class Actionnable : MonoBehaviour
{

    [SerializeField] private UnityEvent _onHoverIn;
    [SerializeField] private UnityEvent _onHoverOut;
    [SerializeField] private UnityEvent _onActionSucceed;
    [SerializeField] private UnityEvent _onActionYellowSucceed;
    [SerializeField] private UnityEvent _onActionFailed;

    // Update is called once per frame
    public void HoverIn()
    {
        RegisterToActionManager();
        QTEManager.Instance.StartQTE(gameObject);
        _onHoverIn?.Invoke();
    }
    public void HoverOut()
    {
        UnregisterFromActionManager();
        QTEManager.Instance.Interrupt();
        _onHoverOut?.Invoke();
    }
    private void Succeed()
    {
        UnregisterFromActionManager();
        _onActionSucceed?.Invoke();
    }
    private void YellowSucceed()
    {
        UnregisterFromActionManager();
        _onActionYellowSucceed?.Invoke();
    }
    private void Failed()
    {
        UnregisterFromActionManager();
        _onActionFailed?.Invoke();
    }

    private void RegisterToActionManager()
    {
        QTEManager.Instance.OnSuccess += Succeed;
        QTEManager.Instance.OnYellowSuccess += YellowSucceed;
        QTEManager.Instance.OnFailure += Failed;
    }
    private void UnregisterFromActionManager()
    {
        QTEManager.Instance.OnSuccess -= Succeed;
        QTEManager.Instance.OnYellowSuccess -= YellowSucceed;
        QTEManager.Instance.OnFailure -= Failed;
    }
    
}
