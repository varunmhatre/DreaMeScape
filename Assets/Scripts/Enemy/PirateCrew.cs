using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCrew : MonoBehaviour
{
    int x;
    int y;
    int damage;
    int health;
    int noOfTurns;

    void Start()
    {
        damage = 2;
        health = 10;
        noOfTurns = 3;
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
