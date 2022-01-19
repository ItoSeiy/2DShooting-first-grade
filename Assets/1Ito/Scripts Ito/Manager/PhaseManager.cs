using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private IState _state;

    private void Start()
    {
        _state = GetComponent<Phase1>();
        ExcuteMethod2();
        CurrentState();
        ExcuteMethod1();
    }

    // 現在のStateを返します。
    public void CurrentState()
    {
        Debug.Log($"現在のStateは{_state.GetType().Name}です");
    }

    // Stateを変更します。
    public void ChangeState(IState state)
    {
        _state = state;
        Debug.Log($"Stateが{ _state.GetType().Name}に変更されました");
    }

    // それぞれのStateにおけるMethodを実行します。
    public void ExcuteMethod1()
    {
        _state.Method1();
    }

    public void ExcuteMethod2()
    {
        _state = _state.Method2();
    }
}
