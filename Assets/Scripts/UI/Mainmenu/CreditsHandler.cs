using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsHandler : MonoBehaviour
{
    [SerializeField] public GameObject[] allCreditsPieces;
    private Vector3[] startingPositions;
    private float creditsIncrement;
    [SerializeField] private float creditsMaximum;
    [SerializeField] private float timeToWait;

    private float timer;
    private bool isWaiting;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        isWaiting = false;
        startingPositions = new Vector3[allCreditsPieces.Length];
        creditsIncrement = 100.0f;
        for (int i = 0; i < allCreditsPieces.Length; i++)
        {
            startingPositions[i] = allCreditsPieces[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (allCreditsPieces[allCreditsPieces.Length - 1].transform.position.y <= creditsMaximum)
        {
            for (int i = 0; i < allCreditsPieces.Length; i++)
            {
                allCreditsPieces[i].transform.position += new Vector3(0.0f, creditsIncrement * Time.deltaTime, 0.0f);
            }
        }
        if (allCreditsPieces[allCreditsPieces.Length - 1].transform.position.y > creditsMaximum)
        {
            if (!isWaiting)
            {
                timer = 0.0f;
                isWaiting = true;
            }
            WaitForTime();
        }
    }

    public void WaitForTime()
    {
        if (timer >= timeToWait)
        {
            isWaiting = false;
            timer = 0.0f;
            LoopLocations();
        }

        timer += Time.deltaTime;
    }

    public void LoopLocations()
    {
        for (int i = 0; i < allCreditsPieces.Length; i++)
        {
            allCreditsPieces[i].transform.position = startingPositions[i] - new Vector3(0.0f, 1200.0f, 0.0f);
        }
    }
}
