namespace Inputs
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
}
