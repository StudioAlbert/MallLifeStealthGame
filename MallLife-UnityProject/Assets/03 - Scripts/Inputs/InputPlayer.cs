using NUnit.Framework.Constraints;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreInputs
{
    /// <summary>
    /// Reads GameControls inputs via callbacks internally and exposes the current values as public properties.
    /// Add this component to a persistent GameObject (e.g. GameManager).
    /// </summary>
    public class Player : MonoBehaviour
    {
        private GameControls _controls;

        // --- Axes ---
        public Vector2 Move;
        public Vector2 MoveNormalized;
        public Vector2 CameraOrbit;
        public Vector2 CameraZoom;

        // --- Buttons ---
        private bool _useObjectUp;
        private bool _useObjectDown;
        public bool UseObject;
        public bool Interact;
        public bool ChangeVehicle;
        public bool ChangeObject;

        public bool UseObjectUp => Utils.OneUseValue(ref _useObjectUp);
        public bool UseObjectDown => Utils.OneUseValue(ref _useObjectDown);


        private void Awake()
        {
            _controls = new GameControls();

            _controls.Player.Move.performed += ctx =>
            {
                Move = ctx.ReadValue<Vector2>();
                MoveNormalized = Move.normalized;
            };
            _controls.Player.Move.canceled += _ =>
            {
                Move = Vector2.zero;
                MoveNormalized = Vector2.zero;
            };

            _controls.Player.CameraOrbit.performed += ctx => CameraOrbit = ctx.ReadValue<Vector2>();
            _controls.Player.CameraOrbit.canceled += _ => CameraOrbit = Vector2.zero;

            _controls.Player.CameraZoom.performed += ctx => CameraZoom = ctx.ReadValue<Vector2>();
            _controls.Player.CameraZoom.canceled += _ => CameraZoom = Vector2.zero;

            _controls.Player.UseObject.started += _ =>
            {
                _useObjectUp = true;
                UseObject = true;
                _useObjectDown = false;
            };
            _controls.Player.UseObject.canceled += _ =>
            {
                _useObjectUp = false;
                UseObject = false;
                _useObjectDown = true;
            };

            _controls.Player.Interact.started += _ => Interact = true;
            _controls.Player.Interact.canceled += _ => Interact = false;

            _controls.Player.ChangeVehicle.started += _ => ChangeVehicle = true;
            _controls.Player.ChangeVehicle.canceled += _ => ChangeVehicle = false;

            _controls.Player.ChangeObject.started += _ => ChangeObject = true;
            _controls.Player.ChangeObject.canceled += _ => ChangeObject = false;

        }

        private void OnEnable() => _controls.Player.Enable();
        private void OnDisable() => _controls.Player.Disable();
        private void OnDestroy() => _controls.Dispose();


    }
}
