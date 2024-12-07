using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardAcceleration = 1f;
    [SerializeField] float maxForwardVelocity = 10f;
    [SerializeField] float minForwardVelocity = 1f;
    [SerializeField] float verticalVelocityCh = 1f;

    [SerializeField] InputActionReference upperCut;
    [SerializeField] InputActionReference punch;
    [SerializeField] InputActionReference jump;

    [SerializeField] HitCollider hitColliderPunch;
    [SerializeField] HitCollider HitColliderUpperCut;
    [SerializeField] HitCollider HitColliderSmash;

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

        upperCut.action.Enable();
        upperCut.action.performed += OnUpperCut;
    }
    // Update is called once per frame
    void Update()
    {

        Debug.Log(forwardVelocity);
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
 

    private void OnPunch(InputAction.CallbackContext context)
    {
        
        
       StartCoroutine(RightPunch());
        
    }
    private void OnUpperCut(InputAction.CallbackContext context)
    {


        if (characterController.isGrounded)
        {
            verticalVelocity = jumpForce;
            StartCoroutine(UpperCut());

        }
        else
        {
            StartCoroutine(Smash());
        }

    }
    void OnJump(InputAction.CallbackContext ctx)
    {

        if (characterController.isGrounded)
        {

            anim.SetBool("isJumping", true);

            verticalVelocity = jumpForce;

        }
    }

    public IEnumerator RightPunch()
    {
        hitColliderPunch.gameObject.SetActive(true);
        anim.SetTrigger("Punch");
        if (forwardVelocity < maxForwardVelocity)
        {
            forwardVelocity += 1f;
        }
        DOVirtual.DelayedCall(0.5f,
            () => hitColliderPunch.gameObject.SetActive(false));
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator UpperCut()
    {
        HitColliderUpperCut.gameObject.SetActive(true);
        anim.SetTrigger("Uppercut");
        if (forwardVelocity > minForwardVelocity)
        {
            forwardVelocity -= 5f;
        }
        DOVirtual.DelayedCall(0.5f,
            () => HitColliderUpperCut.gameObject.SetActive(false));
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator Smash()
    {
        HitColliderSmash.gameObject.SetActive(true);
        anim.SetTrigger("Uppercut");
        if (forwardVelocity > minForwardVelocity)
        {
            forwardVelocity -= 5f;
        }
        DOVirtual.DelayedCall(0.5f,
            () => HitColliderSmash.gameObject.SetActive(false));
        yield return new WaitForSeconds(0.5f);
    }

    private void OnDisable()
    {
        jump.action.performed -= OnJump;
        jump.action.canceled -= OnJump;


        jump.action.Disable();

        punch.action.Disable();
        punch.action.performed -= OnPunch;

        upperCut.action.Disable();
        upperCut.action.performed -= OnUpperCut;
    }
}
