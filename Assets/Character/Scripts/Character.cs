using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] InputActionReference jump;

    public float speed;

    Animator anim;

    CharacterController controller;

    float verticalVelocity;
    const float gravity = -9.81f;
    public float jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {

        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;

    }
    void Start()
    {
        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.forward * speed * Time.fixedDeltaTime +
        Vector3.up * verticalVelocity * Time.deltaTime;
        controller.Move(movement);


        if (controller.isGrounded)
        {
            verticalVelocity = 0;
        }

        verticalVelocity += gravity * Time.deltaTime;
        anim.SetBool("isJumping", !controller.isGrounded);
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        
            if (controller.isGrounded)
            {

                anim.SetBool("isJumping", true);

                verticalVelocity = jumpForce;

            }
    }


        private void OnDisable()
    {

        jump.action.performed -= OnJump;
        jump.action.canceled -= OnJump;


        jump.action.Disable();

    }
}
