using UnityEngine;

public class NextPiece : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScenarioGenerator.nextPieceReached = true;
        }
    }
}
