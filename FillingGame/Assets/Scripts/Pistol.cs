using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Pistol : MonoBehaviour
{
    [Header("ChechSpeed")]
    [SerializeField] GameObject middlePoint;
    [SerializeField] GameObject rotPoint;   //Rotasyonun yonunu ayarlamak icin kullanilan nokta.
    public float torque;


    [SerializeField] ParticleSystem colaParticle;
    [Header("Fire Rate Options")]
    public float fireRate;
    float nextTimeToFire;
    bool shoot;
    bool isGround = false;

    [Header("Force Options")]
    [SerializeField] float forceMultiplier;
    [SerializeField] Transform forcePoint;
    
    [SerializeField] float onGroundForceMultiplier;
    [SerializeField] Animator animator;
    [System.NonSerialized] public bool finished;

    Rigidbody rb;

    public float rotationForce;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
        animator = animator.gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        
        nextTimeToFire = Time.time;
    }

    void Update()
    {
        

        if (GameManager.Current == null || !GameManager.Current.gameActive)
        {

            return;
        }
        if (finished)
        {
            GameManager.Current.FinishGame();
        }
        
       

        if (Input.GetMouseButtonDown(0) && GameManager.Current.gameActive && !isGround )
        {
            

            if (Time.time > nextTimeToFire)
            {
                Debug.Log(isGround);
                nextTimeToFire = Time.time + fireRate;
                shoot = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && isGround)
        {
            isGround = false;
            colaParticle.Play();
            animator.SetTrigger("Shoot");
            
            rb.AddForce(new Vector3(0, 25f, 0), ForceMode.Impulse);
            
            Debug.Log("Guc uygulandi.");
        } 

    }
    void FixedUpdate()
    {
        if (GameManager.Current == null || !GameManager.Current.gameActive)
        {
            
            return;
        }
        
        if (!isGround)
        {
            
            float posDif = middlePoint.gameObject.transform.position.x - rotPoint.gameObject.transform.position.x;
            if (posDif > 0.25f)
            {
                Physics.gravity = new Vector3(+2, -11.3f, 0);
            }
            else if (posDif < -0.20f)
            {
                Physics.gravity = new Vector3(-1f, -11.3f, 0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (posDif > 0.25f)
                {
                    rb.AddTorque(0, 0, -torque, ForceMode.Impulse);
                }
                else if (posDif < -0.20f)
                {
                    Physics.gravity = new Vector3(-1.5f, -11.3f, 0);
                    rb.AddTorque(0, 0, torque, ForceMode.Impulse);

                }
            }
            
        }
        else if (isGround)
        {
            float posDif = middlePoint.gameObject.transform.position.x - rotPoint.gameObject.transform.position.x;
            Physics.gravity = new Vector3(0, -10.5f, 0);
            if (Input.GetMouseButtonDown(0))
            {
                
                if (posDif > 0.25f)
                {

                    rb.AddTorque(0, 0, torque * 2, ForceMode.Impulse);

                }
                else if (posDif < -0.20f)
                {
                    rb.AddTorque(0, 0, -torque * 2, ForceMode.Impulse);

                }
            }
            
        }

        
        
        if (shoot )
        {
            Shoot();
            shoot = false;
        }

        if (transform.position.x >30f)
        {
            Physics.gravity = new Vector3(0, -10.5f, 0);
        }
    }


    void Shoot()
    {
        
        colaParticle.Play();
        animator.SetTrigger("Shoot");

        rb.AddForceAtPosition((transform.right * forceMultiplier), forcePoint.position, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cliff"))
        {
            
            GameManager.Current.GameOver();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGround = true;
            
        }
    }

}
