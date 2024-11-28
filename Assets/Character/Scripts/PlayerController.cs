using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardAcceleration = 1f;
    [SerializeField] float maxForwardVelocity = 10f;
    [SerializeField] float minForwardVelocity = 10f;
    [SerializeField] float verticalVelocityCh = 1f;

    [SerializeField] InputActionReference punch;
    [SerializeField] InputActionReference jump;

    [SerializeField] HitCollider hitColliderPunch;

    Animator anim;

    public float jumpForce;

    float forwardVelocity = 0f;
    float verticalVelocity = 0f;

    float gravity = -9.8f;

    CharacterController characterController;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;

        punch.action.Enable();
        punch.action.performed += OnPunch;
    }
    // Update is called once per frame
    void Update()
    {
       

       
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.forward * forwardVelocity * Time.fixedDeltaTime +
        Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(movement);

        if (forwardVelocity < minForwardVelocity)
        {
            forwardVelocity += forwardAcceleration * Time.deltaTime;
        }

        if (characterController.isGrounded)
        {
            verticalVelocity = 0;
        }

        verticalVelocity += gravity * Time.deltaTime;
        anim.SetBool("isJumping", !characterController.isGrounded);
    }
    private void OnDisable()
    {
        jump.action.performed -= OnJump;
        jump.action.canceled -= OnJump;


        jump.action.Disable();

        punch.action.Disable();
        punch.action.performed -= OnPunch;
    }

    private void OnPunch(InputAction.CallbackContext context)
    {
        hitColliderPunch.gameObject.SetActive(true);
        DOVirtual.DelayedCall(0.5f, 
            () => hitColliderPunch.gameObject.SetActive(false));
    }
    void OnJump(InputAction.CallbackContext ctx)
    {

        if (characterController.isGrounded)
        {

            anim.SetBool("isJumping", true);

            verticalVelocity = jumpForce;

        }
    }
}
