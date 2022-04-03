using System;
using System.Linq;

namespace Overdose.Data
{
    [Serializable]
    public class SaveData
    {
        public SaveData(int player1StageCount, int player2StageCount)
        {
            Player1StageActives = new bool[player1StageCount];
            Player2StageActives = new bool[player2StageCount];

            Player1StageActives[0] = true;
            Player2StageActives[0] = true;
        }

        /// <summary>
        /// プレイヤー1のステージの解放状態
        /// インデックス 0 -> ステージ1
        /// インデックス 1 -> ステージ2
        /// インデックス 4 -> ステージ5
        /// </summary>
        public bool[] Player1StageActives;

        /// <summary>
        /// プレイヤー2のステージの解放状態
        /// インデックス 0 -> ステージ1
        /// インデックス 1 -> ステージ2
        /// インデックス 4 -> ステージ5
        /// </summary>
        public bool[] Player2StageActives;

    }
}