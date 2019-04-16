using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffects : MonoBehaviour
{

    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float accelChangeAmt;

    private Star[] allStars;
    [SerializeField] private GameObject[] allStarObjs;

    public struct Star
    {
        public GameObject starObj;
        public float posX;
        public float posY;
        public float speedX;
        public float speedY;
        public float accelerationX;
        public float accelerationY;
    }


    // Start is called before the first frame update
    void Start()
    {
        allStars = new Star[allStarObjs.Length];

        for (int i = 0; i < allStars.Length; i++)
        {
            allStars[i].starObj = allStarObjs[i];
            allStars[i].posX = allStars[i].starObj.transform.position.x;
            allStars[i].posY = allStars[i].starObj.transform.position.y;
            allStars[i].speedX = 0.0f;
            allStars[i].speedY = 0.0f;
            allStars[i].accelerationX = 0.0f;
            allStars[i].accelerationY = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allStars.Length; i++)
        {
            ModifyAcceleration(i, accelChangeAmt);
            CalcMovement(i);
        }
    }

    public void ModifyAcceleration(int starNum, float amt)
    {
        //override by slowing down
        if (allStars[starNum].accelerationX* allStars[starNum].accelerationX + allStars[starNum].accelerationY* allStars[starNum].accelerationY > maxAcceleration*maxAcceleration)
        {
            if (allStars[starNum].accelerationX > 0)
            {
                allStars[starNum].accelerationX -= amt;
            }
            else if (allStars[starNum].accelerationX < 0)
            {
                allStars[starNum].accelerationX += amt;
            }

            if (allStars[starNum].accelerationY > 0)
            {
                allStars[starNum].accelerationY -= amt;
            }
            else if (allStars[starNum].accelerationY < 0)
            {
                allStars[starNum].accelerationY += amt;
            }
        }
        //otherwise change randomly
        else
        {
            int positiveX = (int)Mathf.Round(Random.value);
            int positiveY = (int)Mathf.Round(Random.value);

            if (positiveX >= 1)
            {
                allStars[starNum].accelerationX += 0.5f*amt;
            }
            else
            {
                allStars[starNum].accelerationX -= 0.5f*amt;
            }

            if (positiveY >= 1)
            {
                allStars[starNum].accelerationY += 0.5f * amt;
            }
            else
            {
                allStars[starNum].accelerationY -= 0.5f * amt;
            }
        }
    }

    public void CalcMovement(int starNum)
    {
        if (allStars[starNum].speedX * allStars[starNum].speedX + allStars[starNum].speedY * allStars[starNum].speedY < maxSpeed * maxSpeed)
        {
            allStars[starNum].speedX += allStars[starNum].accelerationX;
            allStars[starNum].speedY += allStars[starNum].accelerationY;
        }
        else
        {
            if (allStars[starNum].speedX > 0)
            {
                allStars[starNum].speedX -= accelChangeAmt;
            }
            else
            {
                allStars[starNum].speedX += accelChangeAmt;
            }

            if (allStars[starNum].speedY > 0)
            {
                allStars[starNum].speedY -= accelChangeAmt;
            }
            else
            {
                allStars[starNum].speedY += accelChangeAmt;
            }
        }

        allStars[starNum].posX += allStars[starNum].speedX;
        allStars[starNum].posY += allStars[starNum].speedY;

        allStars[starNum].starObj.transform.position = new Vector3(allStars[starNum].posX, allStars[starNum].posY, allStars[starNum].starObj.transform.position.z);
    }
}
