using System;
using System.Collections.Generic;
using Sensors;
using UnityEngine;

public class StealthController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sensor _stealthSensor;
    [SerializeField] private CoreInputs.Player _inputPlayer;

    
    private Actionnable _actionnable;

    
    // Update is called once per frame
    void Update()
    {

        // If Not null -------------------------
        if (_stealthSensor.Object)
        {
            // Attrapé, est il un objet à activer ?
            _stealthSensor.Object.TryGetComponent(out Actionnable newActivable);
            // oui, je l'active
            if (newActivable && newActivable != _actionnable) Activate(newActivable);
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
