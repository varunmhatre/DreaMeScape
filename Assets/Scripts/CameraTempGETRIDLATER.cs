using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTempGETRIDLATER : MonoBehaviour
{
    [SerializeField] public GameObject[] locations;

    [SerializeField] private float moveSpeed;

    private bool stopping;
    private float timer;

    [SerializeField] private float stopTime;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        stopping = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (index < locations.Length && !stopping)
        {
            MoveToNextLoc(locations[index].transform.position);
        }

        if (stopping == true)
        {
            StopForTime(stopTime);
        }
        else
        {
            timer = 0.0f;
        }

    }

    public void MoveToNextLoc(Vector3 goal)
    {
        
        Vector3 goTo = goal - transform.position;
        Vector3 direction = goTo.normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        Vector3 newGoTo = goal - transform.position;
        Vector3 newDirection = newGoTo.normalized;

        if (direction != newDirection)
        {
            transform.position = goal;
        }
        if (transform.position == goal)
        {
            index++;
            stopping = true;
        }
    }

    public void StopForTime(float time)
    {
        if (timer >= time)
        {
            stopping = false;
            timer = 0.0f;
        }

        timer += Time.deltaTime;
    }
}
