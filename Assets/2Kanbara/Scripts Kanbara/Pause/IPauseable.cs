using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一時停止機能を可能にするインターフェース
/// 
/// 一時停止を可能にする手順
/// 
/// 1,このインタフェイスを継承する
/// 2,PauseResume関数に一時停止、再開後の動きを記述する
/// 引数の bool isPause を用いてif文で分岐させて処理を行う
/// 
/// 3,OnEnable関数で PauseManager.Instance.SetEvent(this); と記述
/// 4,OnDisable関数でPauseManager.Instance.RemoveEvent(this); と記述
/// </summary>
public interface IPauseable
{
    void PauseResume(bool isPause);
}
