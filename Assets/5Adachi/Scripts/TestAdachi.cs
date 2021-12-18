using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAdachi : MonoBehaviour
{
    [SerializeField]int i = 1;
    int i2 = 5;

    int i3;
    void Start()
    {
        Debug.Log(i3);
        i3 = Calculation(i, i2);
        Debug.Log(i3);
    }

    int Calculation(int a, int b)
    {
        int sum = default;
        sum = a + b;
        return sum;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
