using System.Collections.Generic;
using Sensors;
using UnityEngine;

public class StealthController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sensor _stealthSensor;
    [SerializeField] private Inputs.Player _inputPlayer;
    
    private Activable _activable;
    
    // Update is called once per frame
    void Update()
    {
        // If Not null -------------------------
        if (_stealthSensor.Object)
        {
            _stealthSensor.Object.TryGetComponent(out Activable newActivable);
            if (newActivable && newActivable != _activable)
            {
                _activable?.HoverOut();
                _activable = newActivable;
                _activable?.HoverIn();
            }
            if (!newActivable)
            {
                _activable?.HoverOut();
                _activable = null;
            }
        }
        else
        {
            _activable?.HoverOut();
            _activable = null;
        }
        
    }
}
