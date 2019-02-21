using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject cannonball;
    List<GameObject> cannonballs;

    void Start()
    {
        int noOfCannonballs = transform.parent.GetComponent<CannonScript>().charge;
        transform.parent.GetComponent<CannonScript>().cannonballScript = this;
        LoadCannonBalls(noOfCannonballs);
    }

    void LoadCannonBalls(int numberOfCannonballs)
    {
        Vector3 posToInstantiate = transform.position;
        cannonballs = new List<GameObject>();
        for (int i = 0; i < numberOfCannonballs; i++)
        {
            cannonballs.Add(Instantiate(cannonball, posToInstantiate, Quaternion.identity, transform));
            //cannonballs[i].transform.localPosition = posToInstantiate;
            posToInstantiate.z += 0.3f;
        }
        cannonballs.Reverse();
    }

    public void RemoveCannonBall()
    {
        Destroy(cannonballs[0]);
        cannonballs.RemoveAt(0);
    }
}
