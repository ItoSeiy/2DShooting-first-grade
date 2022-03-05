using UnityEngine;

namespace Overdose.Calculation
{
    public static class Calculator
    {
        public static bool RandomBool(int rate)
        {
            if(Random.Range(0, 100) < rate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

