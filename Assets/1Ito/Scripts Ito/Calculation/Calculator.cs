using UnityEngine;

namespace Overdose.Calculation
{
    /// <summary>
    /// 計算をするクラス
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// 渡された確率でbool true を返す
        /// </summary>
        /// <param name="probability">trueを返す確率  min -> 0 max -> 100</param>
        /// <returns></returns>
        public static bool RandomBool(int probability)
        {
            if(probability < 0 || probability > 100)
            {
                Debug.LogWarning($"Calculator.RandomBoolに渡された確率の値が大きい又は小さい可能性があります\n渡された確率{probability}");
            }

            if(Random.Range(0, 100) < probability)
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