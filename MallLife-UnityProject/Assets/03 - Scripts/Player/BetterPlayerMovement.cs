using UnityEngine;

public class BetterPlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CoreInputs.Player _inputPlayer;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement")]
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private AnimationCurve _turnCurve;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotSpeed = 75;

    private Camera _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.angularVelocity = transform.up * (_rotSpeed * _turnCurve.Evaluate(_inputPlayer.Move.x));
        _rigidbody.linearVelocity = transform.forward * (_moveSpeed * _moveCurve.Evaluate(_inputPlayer.Move.y));
    }
}
