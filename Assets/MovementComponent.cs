using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace Character
{
    
    public class MovementComponent : MonoBehaviour
    {

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float runSpeed;

        [SerializeField]
        private float jumpForce;

        private Vector2 inputVector;

        private PlayerController playCon;

        private MyCons myCons;

        private Animator animr;

        private Rigidbody rb;

        private NavMeshAgent playerAgent;

        public LayerMask JumpLayerMask;
        public float JumpThreshold = 0.1f;


        // Animator Hashes

        private readonly int x = UnityEngine.Animator.StringToHash("x");
        private readonly int y = UnityEngine.Animator.StringToHash("y");

        private Vector3 moveDir;

        private void Awake()
        {
            inputVector = Vector2.zero;

            myCons = new MyCons();
            playCon = GetComponent<PlayerController>();

            animr = GetComponent<Animator>();

            rb = GetComponent<Rigidbody>();

            playerAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(playCon.IsJumping)
            {
                return;
            }
            if(!(inputVector.magnitude > 0))
            {
                moveDir = Vector3.zero;
            }

            moveDir = transform.forward * inputVector.y + transform.right * inputVector.x;
            float currentSpeed = playCon.IsRunning ? runSpeed : moveSpeed;

            Vector3 movementDir = moveDir * (currentSpeed * Time.deltaTime);

            //Vector3 move = 

            //transform.position += movementDir * currentSpeed;
            playerAgent.Move(movementDir * currentSpeed);
        }

        private NewControls Controls;
        // Start is called before the first frame update

        public void Start()
        {
            Controls = new NewControls();
            Controls.Enable();

            //Controls.ThirdPerson.Move.performed += MovementPerformed;
        }

        public void OnMove(InputValue value)
        {
            //Debug.Log(value.Get<Vector2>());
            inputVector = value.Get<Vector2>();
           //float pressed = value.ReadValue<float>();

            //if (pressed == 1)
            {
                animr.SetFloat(x, inputVector.x);
                animr.SetFloat(y, inputVector.y);
            }
 
        }

        public void OnRun(InputAction.CallbackContext val)
        {

            // Just set both bools to isPressed!
            if (val.ReadValue<float>() == 1)
            {
                playCon.IsRunning = true;
                animr.SetBool("isRunning", true);
            }
            else
            {
                playCon.IsRunning = false;

                animr.SetBool("isRunning", false);
            }
        }

        public void OnJump(InputValue val)
        {
            //if (val.isPressed) return;
            if(playCon.IsJumping)
            {
                return;
            }

            playerAgent.isStopped = true;
            playerAgent.enabled = false;

            playCon.IsJumping = true;
            animr.SetBool("isJumping", true);
            rb.AddForce((transform.up + moveDir) * jumpForce, ForceMode.Impulse);

            InvokeRepeating(nameof(LandingCheck), 0.7f, 0.1f);
        }

        private void LandingCheck()
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 100.0f, JumpLayerMask))
            {
                if(hit.distance < JumpThreshold)
                {
                    playerAgent.enabled = true;
                    playerAgent.isStopped = false;

                    playCon.IsJumping = false;
                    animr.SetBool("isJumping", false);

                    CancelInvoke(nameof(LandingCheck));
                }
                else
                {
                    return;
                }
            }
        }

        public void OnEnable()
        {
            myCons.Enable();

            //myCons.ThirdPerson.Move.performed += OnMove;
        }

        private void MovementPerformed(InputAction.CallbackContext obj)
        {
            //Debug.Log(obj.ReadValue<Vector2>());
        }

        private void OnDisable()
        {
            //myCons.ThirdPerson.Move.performed -= OnMove;
            myCons.Disable();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Ground"))
            {
                return;
            }

            //playCon.IsJumping = false;
            //animr.SetBool("isJumping", false);
        }

    }


}