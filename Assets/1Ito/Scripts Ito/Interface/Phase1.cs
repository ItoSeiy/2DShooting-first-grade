using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("Phase1‚Ìó‘Ô‚ÅMethod1‚ªÀs‚³‚ê‚Ü‚µ‚½");
    }

    IState IState.Method2()
    {
        Debug.Log("Phase1‚Ìó‘Ô‚ÅMethod2‚ªÀs‚³‚ê‚Ü‚µ‚½");
        Debug.Log("Phase‚ªPhase2‚É‘JˆÚ‚µ‚Ü‚·");
        return GetComponent<Phase2>();
    }
}
