using System;
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
    private void OnResult(ActionResult result)
    {
        UnregisterFromActionManager();
        
        switch (result)
        {
            case ActionResult.Success:
                _onActionSucceed?.Invoke();
                break;
            case ActionResult.MediumFailed:
                _onActionYellowSucceed?.Invoke();
                break;
            case ActionResult.Failed:
                _onActionFailed?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }
    }
    private void RegisterToActionManager() => QTEManager.Instance.OnQteEndedResult += OnResult;
    private void UnregisterFromActionManager() => QTEManager.Instance.OnQteEndedResult -= OnResult;
    
}
