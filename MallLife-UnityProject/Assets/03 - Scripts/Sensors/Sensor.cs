using UnityEngine;

namespace Sensors
{
    public abstract class Sensor : MonoBehaviour
    {
        [SerializeField] protected Color _gizmoColor = Color.blue;
        [SerializeField] protected Color _objectColor = Color.blue;
        [SerializeField] protected LayerMask _objectsLayerMask;
        
        public GameObject Object {get; protected set;}
        
    }
}
