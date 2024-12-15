using UnityEngine;

public class GameLogic : MonoBehaviour
{

    [SerializeField] GameObject canvasDefeat;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.vidas == 0)
        {
            canvasDefeat.SetActive(true);
            PlayerController.vidas = 3; 
        }
    }
}
