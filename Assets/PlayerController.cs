using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character // this seems unnecessary
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsFiring;
        public bool IsReloading;
        public bool IsRunning;
        public bool IsJumping;


        public ReticleScript reticle => ReticleComponent;
        [SerializeField] private ReticleScript ReticleComponent;

    }
}