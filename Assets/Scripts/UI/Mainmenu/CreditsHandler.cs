using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsHandler : MonoBehaviour
{
    [SerializeField] public GameObject[] allCreditsPieces;
    private Vector3[] startingPositions;
    private float creditsIncrement;
    private float creditsMaximum;


    // Start is called before the first frame update
    void Start()
    {
        startingPositions = new Vector3[allCreditsPieces.Length];
        creditsIncrement = 100.0f;
        creditsMaximum = 4200.0f;
        for (int i = 0; i < allCreditsPieces.Length; i++)
        {
            startingPositions[i] = allCreditsPieces[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allCreditsPieces.Length; i++)
        {
            allCreditsPieces[i].transform.position += new Vector3(0.0f, creditsIncrement * Time.deltaTime, 0.0f);
            Debug.Log(creditsIncrement);
            Debug.Log(allCreditsPieces[i].transform.position.y);
        }
        if (allCreditsPieces[allCreditsPieces.Length - 1].transform.position.y > creditsMaximum)
        {
            for (int i = 0; i < allCreditsPieces.Length; i++)
            {
                allCreditsPieces[i].transform.position = startingPositions[i] - new Vector3(0.0f, 1200.0f, 0.0f);
            }
        }
    }
}
