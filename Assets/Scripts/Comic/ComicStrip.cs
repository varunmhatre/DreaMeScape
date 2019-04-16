using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicStrip : MonoBehaviour
{
    [SerializeField] private float panSpeed;
    [SerializeField] public GameObject[] imgLocations;


    private int index;
    private bool halt;
    private float timer;

    [SerializeField] private float haltDuration;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        halt = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(index<imgLocations.Length && !halt)
        {
            PanToNext(imgLocations[index].transform.position);
        }  


        if(halt == true)
        {
            PauseforTime(haltDuration);
        }
        else
        {
            timer = 0.0f;
        }
    }

    public void PanToNext(Vector3 goal)
    {
        Vector3 target = goal - transform.position;
        Vector3 direction = target.normalized;

        transform.position += direction * panSpeed * Time.deltaTime;

        Vector3 newTarget = goal - transform.position;
        Vector3 newDirection = newTarget.normalized;

        if (direction != newDirection)
        {
            transform.position = goal;
        }
        if(transform.position == goal)
        {
            index++;
            halt = true;
        }
    }

    public void PauseforTime(float time)
    {
        if (timer >= time)
        {
            halt = false;
            timer = 0.0f;
        }

        timer += Time.deltaTime;
    }
}
