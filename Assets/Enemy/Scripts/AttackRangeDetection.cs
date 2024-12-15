using UnityEngine;

public class AttackRangeDetection : MonoBehaviour
{
    EnemigoTerrestreController enemigo;


    // Start is called before the first frame update
    private void Awake()
    {

        enemigo = GetComponentInParent<EnemigoTerrestreController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemigo != null)
            {
                enemigo.isInRange = true;
            }
           
        }


    }
    
}
