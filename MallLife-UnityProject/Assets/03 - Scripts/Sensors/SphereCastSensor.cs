using UnityEngine;
using Sensors;


public class SphereCastSensor : Sensor
{

    [SerializeField] private float _radius;
    [SerializeField] private float _depth;

    // Update is called once per frame
    void Update()
    {
        if (Physics.SphereCast(transform.position, _radius, transform.forward, out RaycastHit hit, _depth, _objectsLayerMask))
            Object = hit.collider.gameObject;
        else
            Object = null;
        
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = _gizmoColor;
        Gizmos.DrawRay(transform.position, _depth * transform.forward);
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.DrawWireSphere(transform.position + _depth * transform.forward, _radius);
        
        if (Object)
        {
            Gizmos.color = _objectColor;
            Gizmos.DrawSphere(Object.transform.position, 0.15f);
        }
    }
}
