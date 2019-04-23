using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public int charge;
    public bool isThisCannonSelected;
    Animator cannonExplosionAnimation;
    ParticleSystem explosionBlast;
    public CannonBall cannonballScript;
    Quaternion rotation;

    [SerializeField] public SpriteRenderer baseVisual;
    [SerializeField] public Sprite onVisual;
    [SerializeField] public Sprite offVisual;

    private void Start()
    {
        //charge = 1;
        rotation = transform.rotation;
        cannonExplosionAnimation = transform.GetChild(0).GetComponent<Animator>();
        explosionBlast = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        //cannonExplosionAnimation.enabled = false;
    }

    private void Update()
    {
        if (isThisCannonSelected)
        {
            baseVisual.sprite = onVisual;
            FaceMouse();
        }
        else
        {
            baseVisual.sprite = offVisual;
        }
    }

    public bool isChargeLeft
    {
        get { return (charge > 0); }
    }

    public void Disengage()
    {
        isThisCannonSelected = false;
        //transform.rotation = rotation;
    }

    public void Attack()
    {
        charge--;
        cannonballScript.RemoveCannonBall();
        explosionBlast.Play();
        cannonExplosionAnimation.SetBool("isPlay", true);
    }

    void FaceMouse()
    {
        Vector3 cannnonVec = Camera.main.WorldToScreenPoint(transform.position);
        cannnonVec = Input.mousePosition - cannnonVec;
        float angle = Mathf.Atan2(cannnonVec.y, cannnonVec.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);
    }
}
