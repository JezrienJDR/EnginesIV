using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMonobehaviour : MonoBehaviour
{
    protected MyCons gameInput;

    protected void Awake()
    {
        gameInput = new MyCons();
    }

    protected void OnEnable()
    {
        gameInput.Enable();
    }

    protected void OnDisable()
    {
        gameInput.Disable();
    }
}
