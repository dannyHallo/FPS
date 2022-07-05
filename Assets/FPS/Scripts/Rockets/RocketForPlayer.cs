using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RocketForPlayer : MonoBehaviour
{
    public GameObject thisRocket;
    public GameObject Flame;
    public GameObject Fire;
    public GameObject EngineExplotion;
    public AudioClip ExplodingNoice;
    public AudioClip ShootingNoice;
    public AudioClip EngineNoice;

    public float Damage = 400f;
    public float blowRange = 40f;
    
    public float originOutSpeed = 1.6f;
    public float originSpeed = 1f;
    public float acceleration = 0.05f;
    public float maxSpeed = 4f;


    private Vector3 posRaw;
    private Vector3 posNow;
    private float outDistance;
    private float risistance = 0;
    private float currentSpeed;
    private bool flag1 = true;
    private float distance;
    private Vector3 pos;
    private bool isBlowed = false;
    private string victimName = "none";
    
    // Start is called before the first frame update
    void Start()
    {
        posRaw = transform.position;
        AudioSource.PlayClipAtPoint(ShootingNoice, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        posNow = transform.position;
        outDistance = Vector3.Distance(posRaw, posNow);
        
        if(outDistance <= 4)
        {
            risistance += 0.2f;
            transform.Translate(Vector3.forward * (originOutSpeed - risistance), Space.Self);
        }
        else if(outDistance <= 7)
        {
            transform.Translate(Vector3.forward * 0.2f, Space.Self);
        }
        else
        {
            Fire.SetActive(true);
            if (flag1)
            {
                Instantiate(EngineExplotion, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(EngineNoice, transform.position);
                flag1 = false;
            }
            currentSpeed = originSpeed;
            
            if(currentSpeed <= maxSpeed)
            {
                currentSpeed += acceleration;
            }
            transform.Translate(Vector3.forward * currentSpeed, Space.Self);

        }

        IfSelfDestruct();





    }

    

    //if failed to hit, destroy itself
    void IfSelfDestruct()
    {
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 4))
        {
            if(hitInfo.collider.tag != "Rocket")
            {
                print(hitInfo.collider.name);
                DestroyRkt();
            }
            
            
        }
        
    }


    public void DestroyRkt()
    {
        GetComponent<SphereCollider>().radius = blowRange;
        BallDestruct bd = thisRocket.GetComponent<BallDestruct>();
        bd.Destroy();
        pos = transform.position;
        pos.y = transform.position.y;
        Instantiate(Flame, pos, transform.rotation);
        AudioSource.PlayClipAtPoint(ExplodingNoice, pos);
        Destroy(this);

    }

    

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.GetComponent<Health>() != null)
        {
            if (victimName != other.name)
            {
                other.GetComponent<Health>().TakeDamage(Damage, gameObject);
                
            }
            victimName = other.name;
        }
    }



}
