using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy(中ボス,ボス)の基底クラス
/// </summary>
public class EnemyBese : MonoBehaviour
{
    public virtual void Attack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }
}
