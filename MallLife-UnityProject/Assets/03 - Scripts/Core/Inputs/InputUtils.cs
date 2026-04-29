using System;
using UnityEngine;

namespace CoreInputs
{
    public static class Utils
    {
        public static bool OneUseValue(ref bool value)
        {

            bool oneUseValue = value;
            value = false;
            return oneUseValue;
        }
    }

    [System.Serializable]
    public class CoreButton
    {
        [SerializeField] private bool _up;
        [SerializeField] private bool _down;
        [SerializeField] private bool _maintained;

        public bool Up => Utils.OneUseValue(ref _up);
        public bool Down => Utils.OneUseValue(ref _down);
        public bool Maintained => _maintained;
        public event Action OnUp;
        public event Action OnDown;
        
        public void Started()
        {
            _up = false;
            _maintained = true;
            _down = true;
            OnUp?.Invoke();
        }
        
        public void Canceled()
        {
            _up = true;
            _maintained = false;
            _down = false;
            OnDown?.Invoke();
        }


        public void Reset()
        {
            _up = false;
            _maintained = false;
            _down = false;
        }
    }
}
