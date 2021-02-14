using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticleScript : InputMonobehaviour
{
    public Vector2 MouseSensitivity;
    public int inverted = 1;
    public Vector2 currentAimPosition { get; private set; }

    private Vector2 ReticleStartingPosition;
    private Vector2 CurrentLookDeltas;

    private float HorizontalOffset;
    private float HorizontalConstraint;
    private float VerticalOffset;
    private float VerticalConstraint;

    [SerializeField, Range(0.0f, 1.0f)]
    private float hConstraintRatio = 0.25f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float vConstraintRatio = 25f;

    private float MinHCon;
    private float MaxHCon;
    private float MinVCon;
    private float MaxVCon;

    private MyCons myCons;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.CursorEnabled)
        {
            AppEvents.Invoke_MouseCursorEnable(false);
        }

        ReticleStartingPosition = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

        HorizontalOffset = (Screen.width * hConstraintRatio) / 2.0f;
        VerticalOffset = (Screen.height * vConstraintRatio) / 2.0f;

        MinHCon = -(Screen.width / 2) + HorizontalOffset;
        MaxHCon = (Screen.width / 2) - HorizontalOffset;

        MinVCon = -(Screen.height / 2) + VerticalOffset;
        MaxVCon = (Screen.height / 2) - VerticalOffset;

    }


    //private void Awake()
    //{
    //    myCons = new MyCons();
    //}

    private new void OnEnable()
    {
        base.OnEnable();
        gameInput.ThirdPerson.Look.performed += OnLook;
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        //Debug.Log(obj.ReadValue<Vector2>());

        Vector2 mouseDelta = obj.ReadValue<Vector2>();

        // Debug.Log(Delta);
        CurrentLookDeltas.x += mouseDelta.x * MouseSensitivity.x;
        if(CurrentLookDeltas.x > MaxHCon)
        {
            CurrentLookDeltas.x = MaxHCon;
        }
        if(CurrentLookDeltas.x < MinHCon)
        {
            CurrentLookDeltas.x = MinHCon;
        }
        CurrentLookDeltas.y += mouseDelta.y * MouseSensitivity.x;
        if (CurrentLookDeltas.y > MaxVCon)
        {
            CurrentLookDeltas.y = MaxVCon;
        }
        if (CurrentLookDeltas.y < MinVCon)
        {
            CurrentLookDeltas.y = MinVCon;
        }
    }

    private new void OnDisable()
    {
        base.OnDisable();
        gameInput.ThirdPerson.Look.performed -= OnLook;
    }


    void Update()
    {
        float reticleXpos = ReticleStartingPosition.x + CurrentLookDeltas.x;
        float reticleYpos = ReticleStartingPosition.y + (CurrentLookDeltas.y * inverted);

        currentAimPosition = new Vector2(reticleXpos, reticleYpos);
        transform.position = currentAimPosition;
    }
}
