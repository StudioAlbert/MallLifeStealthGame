using System;
using System.Collections.Generic;
using Sensors;
using UnityEngine;

public class StealthController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sensor _stealthSensor;
    [SerializeField] private CoreInputs.Player _inputPlayer;

    
    private Activable _activable;

    
    // Update is called once per frame
    void Update()
    {

        // If Not null -------------------------
        if (_stealthSensor.Object)
        {
            // Attrapé, est il un objet à activer ?
            _stealthSensor.Object.TryGetComponent(out Activable newActivable);
            // oui, je l'active
            if (newActivable && newActivable != _activable) Activate(newActivable);
            // Non, alors je desactive le précedent objet
            if (!newActivable)
            {
                Deactivate();
            }
        }
        else
        {
           Deactivate();
        }

    }
    private void Activate(Activable newActivable)
    {
        _activable?.HoverOut();
        _activable = newActivable;
        _activable?.HoverIn();
    }
    private void Deactivate()
    {
        _activable?.HoverOut();
        _activable = null;
    }

}
