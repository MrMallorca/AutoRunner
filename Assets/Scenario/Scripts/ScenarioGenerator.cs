using System.Collections;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour
{
    static public ScenarioGenerator instance;
    [SerializeField] int numPiecesToGenerateOnStart = 4;
    [SerializeField] GameObject[] piecesPrefabs;

    Transform nextPiecePos;

    int numPiecesFinished = 0;

    [SerializeField] bool debugEndOfPieceReached;

    public static bool nextPieceReached = false;
    int numbersToGenerateNextOne = 3;
    
    private void Awake()
    {
        instance = this;
        nextPiecePos = transform;

    }
    void Start()
    {
        for(int i = 0; i < numPiecesToGenerateOnStart; i++) 
        {
            AddNewPiece();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(nextPieceReached)
        {
            CallNextBlock();
        }

        Debug.Log(numbersToGenerateNextOne);
    }

    public void EndOfPieceReached()
    {

        numPiecesFinished++;
        if(numPiecesFinished > 1)
        {
            DestroyOldestPiece();
            AddNewPiece();
        }
    }

    void AddNewPiece()
    {
        GameObject pieceToInstantiate = piecesPrefabs[Random.Range(0, piecesPrefabs.Length)];
        GameObject newPiece = Instantiate(pieceToInstantiate, nextPiecePos.position, nextPiecePos.rotation, transform);
        newPiece.transform.parent = transform;
        nextPiecePos = newPiece.GetComponentInChildren<NextPiece>().transform;
    }

    void DestroyOldestPiece()
    {
        GameObject oldestPiece = transform.GetChild(0).gameObject;
        Destroy(oldestPiece);
    }


   public void CallNextBlock()
    {
        numbersToGenerateNextOne -= 1;
        if(numbersToGenerateNextOne < 0)
        {
            numbersToGenerateNextOne = 0;
        }
        if(numbersToGenerateNextOne == 0)
        {
            EndOfPieceReached();
            numbersToGenerateNextOne = 3;

        }
        nextPieceReached = false;
    }

}
