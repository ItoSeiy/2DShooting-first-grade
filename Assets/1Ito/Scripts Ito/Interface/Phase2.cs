using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("State2‚Ìó‘Ô‚ÅMethod1‚ªÀs‚³‚ê‚Ü‚µ‚½");
    }

    IState IState.Method2()
    {
        Debug.Log("State1‚Ìó‘Ô‚ÅMethod2‚ªÀs‚³‚ê‚Ü‚µ‚½");
        Debug.Log("State‚ªState1‚É‘JˆÚ‚µ‚Ü‚·");
        return new Phase1();
    }
}
