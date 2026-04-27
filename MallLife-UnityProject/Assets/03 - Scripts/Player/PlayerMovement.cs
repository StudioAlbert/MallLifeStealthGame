using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Inputs.Player _inputPlayer;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement")]
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotSpeed = 75;

    private Camera _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveMagnitude = _inputPlayer.Move.magnitude;
        if (moveMagnitude > Mathf.Epsilon)
        {
            // Forward
            _rigidbody.linearVelocity = transform.forward * (_moveSpeed * _moveCurve.Evaluate(moveMagnitude));

            // Rotation
            Quaternion inputRotation = Quaternion.LookRotation(new Vector3(_inputPlayer.MoveNormalized.x, 0, _inputPlayer.MoveNormalized.y));
            Quaternion cameraRotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
            Quaternion rotation = Quaternion.Slerp(_rigidbody.rotation, inputRotation * cameraRotation, Time.deltaTime * _rotSpeed);
            
            _rigidbody.MoveRotation(rotation);
            
        }

    }
}
