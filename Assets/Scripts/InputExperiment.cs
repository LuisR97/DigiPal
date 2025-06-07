using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputExperiment : MonoBehaviour
{
    public InputActionReference fire;

    private void OnEnable()
    {
        fire.action.started += Fire;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("FIRE");
    }
}
