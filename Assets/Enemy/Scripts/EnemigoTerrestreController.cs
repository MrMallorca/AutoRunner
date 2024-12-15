using System.Collections;
using UnityEngine;

public class EnemigoTerrestreController : MonoBehaviour
{
    [SerializeField] HitCollider hitColliderPunch;


    float forwardVelocity = 3f;
    float verticalVelocity = 0f;


    CharacterController enemyController;

    float gravity = -9.8f;

    public bool isInRange;

    BoxCollider attackRange;

    HurtCollider hurtcollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        hurtcollider = GetComponent<HurtCollider>();

        attackRange = GetComponentInChildren<BoxCollider>();

    }

    private void OnEnable()
    {
        hurtcollider.onHitReceived.AddListener(OnHurt);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = -Vector3.forward * forwardVelocity * Time.fixedDeltaTime +
                  Vector3.up  * verticalVelocity * Time.deltaTime;
        enemyController.Move(movement);

        if (enemyController.isGrounded)
        {
            verticalVelocity = 0;
        }

        verticalVelocity += gravity * Time.deltaTime;

        if (isInRange)
        {
            StartCoroutine(PunchEnemy());
        }
        else
        {
        }

    }


    public IEnumerator PunchEnemy()
    {
        forwardVelocity = 0;


        yield return new WaitForSeconds(1);
        hitColliderPunch.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        hitColliderPunch.gameObject.SetActive(false);

        
    }

    void OnHurt(HitCollider hitCol, HurtCollider hurtCol)
    {
        Destroy(gameObject);
    }

}
