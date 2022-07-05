using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyElder : MonoBehaviour
{

    private GameObject Player;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public AudioClip LaunchSound;

    private int i = 0;
    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    private float rotate_angle;
    private float distance;
    private Vector3 PlayerPos;
    private bool attackFlag = false;
    private int c = 0;
    private float maxHealth;
    private float health;
    private float lastHealth;
    private int countFlag = 0;
    private Animator Anim;
    private int rate = 200;
    private float jumpDistance = 0;

    public GameObject RocketSmall;

    public Transform RocketPos1;
    public Transform RocketPos2;
    public Transform RocketPos3;
    public Transform RocketPos4;


    public GameObject Blue;
    public GameObject Green;
    public GameObject Red;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        maxHealth = gameObject.GetComponent<Health>().maxHealth;
        health = maxHealth;
        lastHealth = gameObject.GetComponent<Health>().maxHealth;
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        health = gameObject.GetComponent<Health>().currentHealth;

        distance = Vector3.Distance(transform.position, Player.transform.position);
        //print(distance);
        PlayerPos = Player.transform.position;
        PlayerPos.y = transform.position.y;

        HealthCheck();

        if (lastHealth - health > 0)
        {
            attackFlag = true;
        }
        else if (distance <= 240)
        {
            Attack();
        }
        else if (attackFlag)
        {
            Attack();
            //attackFlag = false;
        }
        lastHealth = health;
    }

    void Attack()
    {
        Move();
        if (i == 0)
        {
            Anim.SetBool("DoStick", true);


            i++;
        }
        else
        {
            if (i == 80)
            {
                Anim.SetBool("DoStick", false);
            }
            if (i == 90)
            {
                AudioSource.PlayClipAtPoint(LaunchSound, RocketPos1.position);
                Instantiate(RocketSmall, RocketPos1.position, RocketPos1.rotation);
                Instantiate(RocketSmall, RocketPos2.position, RocketPos2.rotation);
                Instantiate(RocketSmall, RocketPos3.position, RocketPos3.rotation);
                Instantiate(RocketSmall, RocketPos4.position, RocketPos4.rotation);
                
                i++;
            }
            else if (i <= rate)
            {
                i++;
            }
            else
            {

                i = 0;
                //attackFlag = false;
            }
        }
        
    }

    void Move()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        AutoRotate(2.5f);
        if(distance <= 180)
        {
            attackFlag = false;
        }
        if (distance >= 10)
        {

            transform.Translate(Vector3.forward * 0.10f, Space.Self);
            
        }
        if(distance <= 60)
        {
            TransJump();
        }
        else if (distance < 8)
        {
            
            transform.Translate(Vector3.back * 0.11f, Space.Self);
        }
        else
        {
            
            transform.Translate(Vector3.left * 0.05f, Space.Self);
        }
    }

    void AutoRotate(float rotateSpeed)
    {
        raw_rotation = transform.rotation;       //获取当前角度
        transform.LookAt(PlayerPos);               //一瞬间扭到目标角度
        lookat_rotation = transform.rotation;  //获取目标角度
        transform.rotation = raw_rotation;      //回来
        rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);
        transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, rotateSpeed * Time.fixedDeltaTime);
    }

    void HealthCheck()
    {
        if (health / maxHealth > 0.666)
        {
            countFlag = 1;
            Blue.SetActive(true);
        }
        else if (health / maxHealth <= 0.666 && health / maxHealth > 0.333)
        {
            if (countFlag == 1)
            {
                Heart1.AddComponent<Rigidbody>();
                Heart1.GetComponent<Heart>().Destroy();

                Blue.SetActive(false);
                Green.SetActive(true);

                countFlag = 2;
                rate = 150;
            }

        }
        else if (health / maxHealth <= 0.333 && health / maxHealth > 0)
        {
            if (countFlag == 2)
            {
                Heart2.AddComponent<Rigidbody>();
                Heart2.GetComponent<Heart>().Destroy();
                countFlag = 3;
                rate = 90;

                Green.SetActive(false);
                Red.SetActive(true);
            }

        }
    }

    void TransJump()
    {
        c++;
        if(c == 100)
        {
            jumpDistance = Random.Range(-8f, 8f);
            transform.Translate(Vector3.left * jumpDistance, Space.Self);
            c = 0;
        }
        
    }
}
