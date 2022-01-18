using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : MonoBehaviour, IState
{
    void IState.Method1()
    {

    }

    IState IState.Method2()
    {
        return new Phase2();
    }
}
