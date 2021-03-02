using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{

    public WeaponComponent weaponComp;

    public TMP_Text weaponNameTxt;
    public TMP_Text ClipTxt;
    public TMP_Text AmmoTxt;


    private void OnEnable()
    {
        //PlayerEvents.OnWeaponEquipped += (WeaponComponent weapon) => weaponComp = weapon;
        PlayerEvents.OnWeaponEquipped += OnEquip;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnEquip;
    }

    public void OnEquip(WeaponComponent wc)
    {
        weaponComp = wc;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!weaponComp)
        {
            return;
        }

        weaponNameTxt.text = weaponComp.stats.name;
        AmmoTxt.text = weaponComp.stats.ShotsAvailable.ToString();
        ClipTxt.text = weaponComp.stats.ShotsInClip.ToString();


    }
}
