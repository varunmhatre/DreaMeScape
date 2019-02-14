using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCaptain : MonoBehaviour
{
    int x;
    int y;
    int damage;
    int health;
    int noOfTurns;

    void Start()
    {
        damage = 3;
        health = 15;
        noOfTurns = 5;
    }

    void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            //We could effectively pool it too
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetNumberOfTurns()
    {
        return noOfTurns;
    }
}
