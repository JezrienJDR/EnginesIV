using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GunType
{
    None,
    Pistol,
    AssaultRifle,
    SMG,
    Count
}

[Serializable]
public struct WeaponStats
{
    public GunType type;
    public string name;
    public float damage;
    public int ShotsInClip;
    public int ClipSize;
    public int ShotsAvailable;
    public float FireStartDelay;
    public float FireRate;
    public float Range;
    public bool Repeating;
    public LayerMask layerMask;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform GripLocation => GripIKLocation;

    [SerializeField]
    private Transform GripIKLocation;

    [SerializeField]
    public WeaponStats stats;

    public bool firing = false;
    public bool reloading = false;

    protected WeaponSocket sock;
    protected ReticleScript ret;
    protected Camera MainCam;

    [SerializeField]
    private Transform MuzzleFlashLoc;

    [SerializeField]
    private ParticleSystem MuzzleFlash;

    private void Awake()
    {
        MainCam = Camera.main;
    }

    public void Initialize(WeaponSocket _sock, ReticleScript _ret)
    {
        sock = _sock;
        ret = _ret;
    }

    public virtual void StartFiring()
    {
        if(!stats.Repeating)
        {
            Fire();
        }
        else
        {
            firing = true;
            InvokeRepeating(nameof(Fire), stats.FireStartDelay, stats.FireRate);
            
        }
    }

    public virtual void StopFiring()
    {
        firing = false;
        CancelInvoke(nameof(Fire));
        
    }

    public virtual void Fire()
    {
        MuzzleFlash.Play();
        stats.ShotsInClip--;
    }

    public virtual void StartReloading()
    {
        reloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        reloading = false;
    }

    protected virtual void ReloadWeapon()
    {
        int shotsToReload = stats.ClipSize - stats.ShotsAvailable;

        if(shotsToReload < 0)
        {
            stats.ShotsInClip = stats.ClipSize;
            stats.ShotsAvailable -= stats.ClipSize;
        }
        else
        {
            stats.ShotsInClip = stats.ShotsAvailable;
            stats.ShotsAvailable = 0;
        }
    }
}
