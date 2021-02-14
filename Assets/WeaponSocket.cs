using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Character;

public class WeaponSocket : InputMonobehaviour
{

    [Header("Weapon to Spawn")]
    public GameObject weapon;

    public Transform socket;

    // Components
    private PlayerController playCon;
    private ReticleScript reticle;
    private Animator PlayerAnim;

    private Transform GripIK;

    // References
    private Camera viewCamera;
    private static readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    private static readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private static readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
    private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");


    private new void Awake()
    {
        base.Awake();

        PlayerAnim = GetComponent<Animator>();

        playCon = GetComponent<PlayerController>();
        if(playCon)
        {
            reticle = playCon.reticle;
        }

        viewCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weapon, socket.position, socket.rotation);

        if (!spawnedWeapon) return;

        spawnedWeapon.transform.parent = socket;

        WeaponComponent weaponComp = spawnedWeapon.GetComponent<WeaponComponent>();
        GripIK = weaponComp.GripLocation;
    }


    public void OnReload(InputAction.CallbackContext obj)
    {
        Debug.Log("Reload");
        //PlayerAnim.SetBool("IsReloading", (obj.ReadValue<float>() == 1 ? true : false));
        PlayerAnim.SetBool("Reload", true);
    }

    public void OnFire(InputAction.CallbackContext obj)
    {
        Debug.Log("FIRE");

        PlayerAnim.SetBool("IsFiring", (obj.ReadValue<float>() == 1 ? true : false)) ;

        // Look into Inputvalue.Ispressed
    }

    public void EndReload()
    {
        PlayerAnim.SetBool("Reload", false);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        PlayerAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        PlayerAnim.SetIKPosition(AvatarIKGoal.LeftHand, GripIK.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private new void OnEnable()
    {
        base.OnEnable();
        gameInput.ThirdPerson.Look.performed += OnLook;
        gameInput.ThirdPerson.Reload.performed += OnReload;
        gameInput.ThirdPerson.Fire.performed += OnFire;

    } 

    private new void OnDisable()
    {
        base.OnDisable();
        gameInput.ThirdPerson.Look.performed -= OnLook;
        gameInput.ThirdPerson.Reload.performed -= OnReload;
        gameInput.ThirdPerson.Fire.performed -= OnFire;
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
   
        Vector3 independentMousePos = viewCamera.ScreenToViewportPoint(reticle.currentAimPosition);

        PlayerAnim.SetFloat("AimH", independentMousePos.x);
        PlayerAnim.SetFloat("AimV", independentMousePos.y);

    }

}
