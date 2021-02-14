using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public bool CursorEnabled { get; private set; } = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void EnableCursor(bool enable)
    {
        if(enable)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CursorEnabled = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CursorEnabled = false;
        }
    }

    private void OnEnable()
    {
        AppEvents.MouseCursorEnableEvent += EnableCursor;
    }
    private void OnDisable()
    {
        AppEvents.MouseCursorEnableEvent -= EnableCursor;    
    }
}
