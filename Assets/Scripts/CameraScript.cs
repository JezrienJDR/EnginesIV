using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : InputMonobehaviour
{
    [SerializeField]
    private float RotationPower = 10.0f;
    [SerializeField]
    private float HorizontalDamping = 1.0f;
    [SerializeField]
    private GameObject followTarget;
    private Transform followTargetTransform;

    private Vector2 prevMouseDelta = Vector2.zero;

    private new void Awake()
    {
        base.Awake();
        followTargetTransform = followTarget.transform;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        //gameInput.ThirdPerson.Look.performed += OnLook;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        //gameInput.ThirdPerson.Look.performed -= OnLook;
    }

    //private void OnLook(InputAction.CallbackContext delta)
    private void OnLook(InputValue delta)
    {
        Vector2 aimVal = delta.Get<Vector2>();

        Quaternion addedRotation = Quaternion.AngleAxis(Mathf.Lerp(prevMouseDelta.x, aimVal.x, (1.0f / HorizontalDamping) * RotationPower), Vector3.up);

        followTargetTransform.rotation *= addedRotation;

        prevMouseDelta = aimVal;

        transform.rotation = Quaternion.Euler(0.0f, followTargetTransform.rotation.eulerAngles.y, 0.0f);

        followTargetTransform.localEulerAngles = Vector3.zero;

    }



}
