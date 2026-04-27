using UnityEngine;

namespace Inputs
{
    public class QuickTimeEvents : MonoBehaviour
    {
        private GameControls _controls;

        // Buttons ---------------------
        // private bool _northBtnUp;
        // private bool _northBtnDown;
        // private bool _southBtnUp;
        // private bool _southBtnDown;
        // private bool _eastBtnUp;
        // private bool _eastBtnDown;
        // private bool _westBtnUp;
        // private bool _westBtnDown;
        // private bool _validateUp;
        // private bool _validateDown;
        // private bool _cancelUp;
        // private bool _cancelDown;

        public CoreInputs.CoreButton NorthBtn = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton SouthBtn = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton EastBtn = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton WestBtn = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton Validate = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton Cancel = new CoreInputs.CoreButton();
        // public bool SouthBtn;
        // public bool SouthBtnUp => Utils.OneUseValue(ref _southBtnUp);
        // public bool SouthBtnDown => Utils.OneUseValue(ref _southBtnDown);
        // public bool EastBtn;
        // public bool EastBtnUp => Utils.OneUseValue(ref _eastBtnUp);
        // public bool EastBtnDown => Utils.OneUseValue(ref _eastBtnDown);
        // public bool WestBtn;
        // public bool WestBtnUp => Utils.OneUseValue(ref _westBtnUp);
        // public bool WestBtnDown => Utils.OneUseValue(ref _westBtnDown);

        // public bool Validate;
        // public bool ValidateUp => Utils.OneUseValue(ref _validateUp);
        // public bool ValidateDown => Utils.OneUseValue(ref _validateDown);
        // public bool Cancel;
        // public bool CancelUp => Utils.OneUseValue(ref _cancelUp);
        // public bool CancelDown => Utils.OneUseValue(ref _cancelDown);

        private void Awake()
        {
            _controls = new GameControls();
            _controls.QuickTimeEvents.East.started += _ => EastBtn.Started();
            _controls.QuickTimeEvents.East.canceled += _ => EastBtn.Canceled();

            _controls.QuickTimeEvents.North.started += _ => NorthBtn.Started();
            _controls.QuickTimeEvents.North.canceled += _ => NorthBtn.Canceled();

            _controls.QuickTimeEvents.West.started += _ => WestBtn.Started();
            _controls.QuickTimeEvents.West.canceled += _ => WestBtn.Canceled();

            _controls.QuickTimeEvents.South.started += _ => SouthBtn.Started();
            _controls.QuickTimeEvents.South.canceled += _ => SouthBtn.Canceled();

            _controls.QuickTimeEvents.Validate.started += _ => Validate.Started();
            _controls.QuickTimeEvents.Validate.canceled += _ => Validate.Canceled();

            _controls.QuickTimeEvents.Cancel.started += _ => Cancel.Started();
            _controls.QuickTimeEvents.Cancel.canceled += _ => Cancel.Canceled();

        }
        private void OnEnable() => _controls.QuickTimeEvents.Enable();
        private void OnDisable() => _controls.QuickTimeEvents.Disable();
        private void OnDestroy() => _controls.Dispose();

        public void ResetInputs()
        {
            NorthBtn.Reset();
            SouthBtn.Reset();
            EastBtn.Reset();
            WestBtn.Reset();
            Validate.Reset();
            Cancel.Reset();
            //     _northBtnUp = false;
            //     _northBtnDown = false;
            //     _southBtnUp = false;
            //     _southBtnDown = false;
            //     _eastBtnUp = false;
            //     _eastBtnDown = false;
            //     _westBtnUp = false;
            //     _westBtnDown = false;
            //     
            // _validateUp= false;
            // _validateDown= false;
            // _cancelUp= false;
            // _cancelDown= false;
            //
            //     NorthBtn = false;
            //     SouthBtn = false;
            //     EastBtn = false;
            //     WestBtn = false;
            //     Validate = false;
            //     Cancel = false;
        }

    }
}
