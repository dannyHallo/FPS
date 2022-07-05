using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float Damage = 100f;
    public float height = 10f;
    public float chaseSpeedOrigin = 1f;
    public float turningSpeed = 1f;
    public float blowRange = 2.5f;
    public float triggerDistance = 4;

    private float yRaw;
    private float yNow;
    private float radius = 20f;
    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    private float rotate_angle;
    private GameObject Player;
    private Vector3 PlayerPos;
    private bool flag1 = true;
    private string victimName = "none";


    private int i = 0;
    private int t = 0;
    private float distance;
    private Vector3 pos;
    private float myHeight;
    private float chaseSpeed;
    private float rawDistance = 10000;

    public GameObject thisRocket;
    public GameObject Flame;
    public GameObject Fire;
    public AudioClip ExplodingNoice;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        yRaw = transform.position.y;
        Player = GameObject.Find("Player");
        myHeight = Random.Range(height, height + 5);
        chaseSpeed = chaseSpeedOrigin;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yNow = transform.position.y;
        PlayerPos = Player.transform.position;

        //print(GetComponent<Health>().currentHealth);
        if (yNow - yRaw <= myHeight && flag1)
        {
            transform.Translate(Vector3.forward * 0.45f, Space.Self);
            Fire.SetActive(true);
        }
        else if(yNow - yRaw <= myHeight+5 && flag1)
        {
            transform.Translate(Vector3.forward * 0.25f, Space.Self);
            Fire.SetActive(false);
        }
        else if(flag1)
        {
            Fire.SetActive(true);
            AutoRotate(5f);
            if (rotate_angle <= 15f)
            {
                AutoRotate(15f);
            }
            if (rotate_angle <= 5f)
            {
                flag1 = false;
            }
            

        }
        else
        {
            i++;
            if(i < 30)
            {
                Fire.SetActive(true);
                AutoRotate(1.5f);
            }
            else
            {
                Chase();
                IfSelfDestruct();
            }
            
        }
        
        
    }

    void Chase()
    {
        chaseSpeed += 0.01f;
        AutoRotate(6f);
        transform.Translate(Vector3.forward * chaseSpeed, Space.Self);

    }

    void AutoRotate(float rotateSpeed)
    {
        raw_rotation = transform.rotation;       //获取当前角度
        transform.LookAt(PlayerPos);               //一瞬间扭到目标角度
        lookat_rotation = transform.rotation;  //获取目标角度
        transform.rotation = raw_rotation;      //回来
        rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);
        transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, rotateSpeed * turningSpeed * Time.fixedDeltaTime);
    }

    void IfSelfDestruct()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, triggerDistance))
        {
            //print(hitInfo.collider.name);
            if (hitInfo.collider.tag != "Rocket")
            {
                //print("trigger1");
                DestroyRkt();
            }
            


        }

        distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance > rawDistance)
        {
            //print("trigger2");
            DestroyRkt();
        }
        rawDistance = distance;

    }


    public void DestroyRkt()
    {
        CheckPlayer();
        BallDestruct bd = thisRocket.GetComponent<BallDestruct>();
        bd.Destroy();
        pos = transform.position;
        pos.y = transform.position.y;
        Instantiate(Flame, pos, transform.rotation);
        AudioSource.PlayClipAtPoint(ExplodingNoice, pos);
        Destroy(this);

    }



    void CheckPlayer()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        print(distance);
        if(distance <= blowRange)
        {
            Player.GetComponent<Health>().TakeDamage(Damage, gameObject);
        }
    }
}
