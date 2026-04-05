using System;
using UnityEngine;

namespace Inputs
{
    public class QuickTimeEvents : MonoBehaviour
    {
        private GameControls _controls;

        // Buttons ---------------------
        private bool _northBtnUp;
        private bool _northBtnDown;
        private bool _southBtnUp;
        private bool _southBtnDown;
        private bool _eastBtnUp;
        private bool _eastBtnDown;
        private bool _westBtnUp;
        private bool _westBtnDown;
        private bool _validateUp;
        private bool _validateDown;
        private bool _cancelUp;
        private bool _cancelDown;

        public bool NorthBtn;
        public bool NorthBtnUp => Utils.OneUseValue(ref _northBtnUp);
        public bool NorthBtnDown => Utils.OneUseValue(ref _northBtnDown);
        public bool SouthBtn;
        public bool SouthBtnUp => Utils.OneUseValue(ref _southBtnUp);
        public bool SouthBtnDown => Utils.OneUseValue(ref _southBtnDown);
        public bool EastBtn;
        public bool EastBtnUp => Utils.OneUseValue(ref _eastBtnUp);
        public bool EastBtnDown => Utils.OneUseValue(ref _eastBtnDown);
        public bool WestBtn;
        public bool WestBtnUp => Utils.OneUseValue(ref _westBtnUp);
        public bool WestBtnDown => Utils.OneUseValue(ref _westBtnDown);

        public bool Validate;
        public bool ValidateUp => Utils.OneUseValue(ref _validateUp);
        public bool ValidateDown => Utils.OneUseValue(ref _validateDown);
        public bool Cancel;
        public bool CancelUp => Utils.OneUseValue(ref _cancelUp);
        public bool CancelDown => Utils.OneUseValue(ref _cancelDown);

        private void Awake()
        {
            _controls = new GameControls();
            _controls.QuickTimeEvents.East.started += _ =>
            {
                _eastBtnDown = true;
                EastBtn = true;
                _eastBtnUp = false;
            };
            _controls.QuickTimeEvents.East.canceled += _ =>
            {
                _eastBtnDown = false;
                EastBtn = false;
                _eastBtnUp = true;
            };
            _controls.QuickTimeEvents.North.started += _ =>
            {
                _northBtnDown = true;
                NorthBtn = true;
                _northBtnUp = false;
            };
            _controls.QuickTimeEvents.North.canceled += _ =>
            {
                _northBtnDown = false;
                NorthBtn = false;
                _northBtnUp = true;
            };
            _controls.QuickTimeEvents.West.started += _ =>
            {
                _westBtnDown = true;
                WestBtn = true;
                _westBtnUp = false;
            };
            _controls.QuickTimeEvents.West.canceled += _ =>
            {
                _westBtnDown = false;
                WestBtn = false;
                _westBtnUp = true;
            };
            _controls.QuickTimeEvents.South.started += _ =>
            {
                _southBtnDown = true;
                SouthBtn = true;
                _southBtnUp = false;
            };
            _controls.QuickTimeEvents.South.canceled += _ =>
            {
                _southBtnDown = false;
                SouthBtn = false;
                _southBtnUp = true;
            };

            _controls.QuickTimeEvents.Validate.started += _ =>
            {
                _validateDown = true;
                Validate = true;
                _validateUp = false;
            };
            _controls.QuickTimeEvents.Validate.canceled += _ =>
            {
                _validateDown = false;
                Validate = false;
                _validateUp = true;
            };
            _controls.QuickTimeEvents.Cancel.started += _ =>
            {
                _cancelDown = true;
                Cancel = true;
                _cancelUp = false;
            };
            _controls.QuickTimeEvents.Cancel.canceled += _ =>
            {
                _cancelDown = false;
                Cancel = false;
                _cancelUp = true;
            };

        }
        private void OnEnable() => _controls.QuickTimeEvents.Enable();
        private void OnDisable() => _controls.QuickTimeEvents.Disable();
        private void OnDestroy() => _controls.Dispose();

        public void ResetInputs()
        {
            _northBtnUp = false;
            _northBtnDown = false;
            _southBtnUp = false;
            _southBtnDown = false;
            _eastBtnUp = false;
            _eastBtnDown = false;
            _westBtnUp = false;
            _westBtnDown = false;
            
        _validateUp= false;
        _validateDown= false;
        _cancelUp= false;
        _cancelDown= false;

            NorthBtn = false;
            SouthBtn = false;
            EastBtn = false;
            WestBtn = false;
            Validate = false;
            Cancel = false;
        }

    }
}
