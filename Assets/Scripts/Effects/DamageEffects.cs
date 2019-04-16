using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] public GameObject[] damageObjs;
    private float[] timers;
    private Vector3[] directions;
    [SerializeField] private float speed;

    public int currIndex;

    // Start is called before the first frame update
    void Start()
    {
        currIndex = 0;

        timers = new float[damageObjs.Length];
        directions = new Vector3[damageObjs.Length];

        for (int i = 0; i < timers.Length; i++)
        {
            timers[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < damageObjs.Length; i++)
        {
            BounceAround(i, timers[i]);
        }
    }

    public void SendToLocation(int index, GameObject damagedObj, int dmgAmt)
    {
        damageObjs[index].GetComponent<TextMesh>().text = dmgAmt.ToString();
        damageObjs[index].transform.position = damagedObj.transform.position;
        timers[index] = 0.0f;
        currIndex++;
        if (currIndex >= damageObjs.Length)
        {
            currIndex = 0;
        }
    }

    public void BounceAround(int index, float time)
    {
        if (time == 0.0f)
        {
            int rand = (int)Mathf.Round(Random.value);
            if (rand == 0)
            {
                directions[index] = new Vector3(Mathf.Cos(Mathf.PI / 3.0f), Mathf.Sin(Mathf.PI / 3.0f), 0.0f);
            }
            else
            {
                directions[index] = new Vector3(Mathf.Cos(2.0f * Mathf.PI / 3.0f), Mathf.Sin(2.0f * Mathf.PI / 3.0f), 0.0f);
            }
        }
        else
        {
            directions[index] += new Vector3(0.0f, 10.0f, 0.0f) * Time.deltaTime;
        }

        directions[index].Normalize();

        damageObjs[index].transform.position += directions[index] * speed * Time.deltaTime;
    }
}
