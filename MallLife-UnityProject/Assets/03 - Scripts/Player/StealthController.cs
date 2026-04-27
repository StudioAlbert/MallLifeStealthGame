using System;
using System.Collections.Generic;
using Sensors;
using UnityEngine;

public class StealthController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sensor _stealthSensor;
    
    private Actionnable _actionnable;
    // Catching new object only happened if trying to catch REAL new object
    // Player has to move to catch a new object, if he remains still, nothing happens
    private bool _allowNewObject = true;
    
    // Update is called once per frame
    void Update()
    {

        // If Not null -------------------------
        if (_stealthSensor.Object)
        {
            // Attrapé, est il un objet à activer ?
            _stealthSensor.Object.TryGetComponent(out Actionnable newActivable);
            // oui, je l'active
            if (_allowNewObject && newActivable && newActivable != _actionnable)
            {
                _allowNewObject = false;
                Activate(newActivable);
            }
            // Non, alors je desactive le précedent objet
            if (!newActivable)
            {
                Deactivate();
            }
        }
        else
        {
           Deactivate();
           _allowNewObject = true;
        }

    }
    private void Activate(Actionnable newActionnable)
    {
        _actionnable?.HoverOut();
        _actionnable = newActionnable;
        _actionnable?.HoverIn();
    }
    private void Deactivate()
    {
        _actionnable?.HoverOut();
        _actionnable = null;
    }

}
