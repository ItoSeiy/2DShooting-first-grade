using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] Button _firstSelectButton;
    void Start()
    {
        _firstSelectButton.Select();
    }
}