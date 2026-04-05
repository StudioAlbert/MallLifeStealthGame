using Sensors;
using UnityEngine;

public class OverlapSphereSensor : Sensor
{

    [SerializeField] private float _radius;
    [SerializeField] private float _depthProjection;

    readonly Collider[] _colliders = new Collider[25];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int nbCollisions = Physics.OverlapSphereNonAlloc(transform.position + transform.forward * _depthProjection,
            _radius,
            _colliders,
            _objectsLayerMask);
        
        if (nbCollisions > 0)
        {
            Debug.Log($"{nbCollisions} Collisions detected.");
            
            float bestScore = float.MinValue;
            int bestColliderIndex = 0;
            
            for (int idxCollider = 0; idxCollider < nbCollisions; idxCollider++)
            {
                Vector3 direction = _colliders[idxCollider].transform.position - transform.position;
                float score = Vector3.Dot(transform.forward, direction) / direction.sqrMagnitude;

                if (score >= bestScore)
                {
                    bestScore = score;
                    bestColliderIndex = idxCollider;
                }

            }

            Object = _colliders[bestColliderIndex].gameObject;
        }
        else
        {
            Object = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (Object)
        {
            Gizmos.color = _objectColor;
            Gizmos.DrawLine(transform.position, Object.transform.position);
            Gizmos.DrawWireSphere(transform.position + _depthProjection * transform.forward, _radius);
        }
        else
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position + _depthProjection * transform.forward, _radius);
        }
    }

}
