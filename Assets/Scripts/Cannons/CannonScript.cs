using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public int charge = 1;
    public bool isThisCannonSelected;
    Animator cannonExplosionAnimation;
    ParticleSystem explosionBlast;

    Quaternion rotation;
    public bool test;

    private void Start()
    {
        rotation = transform.rotation;
        cannonExplosionAnimation = transform.GetChild(0).GetComponent<Animator>();
        explosionBlast = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        //cannonExplosionAnimation.enabled = false;
    }

    private void Update()
    {
        if (isThisCannonSelected)
        {
            FaceMouse();
        }
        if (test)
        {
            Attack();
            test = false;
        }
    }

    public bool isChargeLeft
    {
        get { return (charge > 0); }
    }

    public void Disengage()
    {
        isThisCannonSelected = false;
        transform.rotation = rotation;
    }

    public void Attack()
    {
        charge--;
        explosionBlast.Play();
        //cannonExplosionAnimation.enabled = true;
        cannonExplosionAnimation.SetBool("isPlay", true);
        Disengage();
    }

    void FaceMouse()
    {
        Vector3 cannnonVec = Camera.main.WorldToScreenPoint(transform.position);
        cannnonVec = Input.mousePosition - cannnonVec;
        float angle = Mathf.Atan2(cannnonVec.y, cannnonVec.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);
    }
}
