using UnityEngine;

namespace Inputs
{
    public class InputMenus : MonoBehaviour
    {

        private GameControls _controls;

        // Menus -----------------------------------
        public CoreInputs.CoreButton Inventory = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton Cancel = new CoreInputs.CoreButton();
        public CoreInputs.CoreButton Confirm = new CoreInputs.CoreButton();
        public bool Objectives;
        public bool StealthView;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _controls = new GameControls();

            _controls.QuickMenus.Inventory.started += _ => Inventory.Started();
            _controls.QuickMenus.Inventory.canceled += _ => Inventory.Canceled();

            _controls.UI.Cancel.started += _ => Cancel.Started();
            _controls.UI.Cancel.canceled += _ => Cancel.Canceled();
            
            _controls.UI.Confirm.started += _ => Confirm.Started();
            _controls.UI.Confirm.canceled += _ => Confirm.Canceled();

            _controls.QuickMenus.Quests.started += _ => Objectives = true;
            _controls.QuickMenus.Quests.canceled += _ => Objectives = false;

            _controls.QuickMenus.StealthView.started += _ => StealthView = true;
            _controls.QuickMenus.StealthView.canceled += _ => StealthView = false;


        }

        private void OnEnable()
        {
            _controls.QuickMenus.Enable();
            _controls.UI.Enable();
        }
        private void OnDisable()
        {
            _controls.QuickMenus.Disable();
            _controls.UI.Disable();
        }
        private void OnDestroy() => _controls.Dispose();

    }
}
