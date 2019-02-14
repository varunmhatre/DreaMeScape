using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;
    [SerializeField] int presence;

    public bool isEncumbered;
    public bool isEnemy;
}
