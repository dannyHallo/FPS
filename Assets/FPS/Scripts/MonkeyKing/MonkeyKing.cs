using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyKingScript : MonoBehaviour
{

    private GameObject Player;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject ActiveEyes;

    public float MeleeDamage = 1.5f;
    public float MoveSpeed = 1.8f;

    private int i = 0;
    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    public float distance;
    private Vector3 PlayerPos;
    private bool attackFlag = false;
    private float meleeDmgAdd = 1f;
    private float moveSpeedAdd = 1f;

    private float maxHealth;
    private float health;
    private float lastHealth;

    private float attackSpeed = 0f;

    private int countFlag = 0;
    private Animator ballForward;

    public GameObject Rocket;
    public GameObject Ball;
    
    public Transform RocketPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        maxHealth = gameObject.GetComponent<Health>().maxHealth;
        health = maxHealth;
        lastHealth = gameObject.GetComponent<Health>().maxHealth;
        ballForward = Ball.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if(distance <= 80)
        {
            ActiveEyes.SetActive(true);
            gameObject.GetComponent<Health>().invincible = false;

            health = gameObject.GetComponent<Health>().currentHealth;
            PlayerPos = Player.transform.position;
            PlayerPos.y = transform.position.y;
            if (health / maxHealth > 0.666)
            {
                countFlag = 1;
            }
            else if (health / maxHealth <= 0.666 && health / maxHealth > 0.333)
            {
                if (countFlag == 1)
                {
                    Heart1.AddComponent<Rigidbody>();
                    Heart1.GetComponent<Heart>().Destroy();
                    moveSpeedAdd = 1.5f;
                    attackSpeed = 1f;
                    meleeDmgAdd = 1.5f;
                    countFlag = 2;
                    
                }

            }
            else if (health / maxHealth <= 0.333 && health / maxHealth > 0)
            {
                if (countFlag == 2)
                {
                    Heart2.AddComponent<Rigidbody>();
                    Heart2.GetComponent<Heart>().Destroy();
                    moveSpeedAdd = 2.5f;
                    attackSpeed = 2f;
                    meleeDmgAdd = 2.5f;
                    countFlag = 3;
                }

            }
            else
            {
            }

            if (lastHealth - health > 0)
            {
                attackFlag = true;
            }
            else if (distance <= 60)
            {
                Attack();
            }
            else if (attackFlag)
            {
                Attack();
                distance = Vector3.Distance(transform.position, Player.transform.position);
                if (distance <= 50)
                {
                    attackFlag = false;
                }
            }
            else
            {
                ballForward.speed = 0;
            }
            MeleeDmg();
            lastHealth = health;
        }
        else
        {
            gameObject.GetComponent<Health>().invincible = true;
        }
        
            
            
        
        
    }

    void Attack()
    {
        Move();
        if (i == 0)
        {
            GameObject thisRocket = Instantiate(Rocket, RocketPos.position, RocketPos.rotation);
            i++;
        }
        else if(i <= 200 - attackSpeed * 50)
        {
            i++;
        }
        else
        {
            i = 0;
        }
    }

    void Move()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        AutoRotate(2.5f);
        if (distance >= 10)
        {
            transform.Translate(Vector3.forward * 0.10f * moveSpeedAdd * MoveSpeed, Space.Self);
            ballForward.speed = 1;
        }
        else if(distance < 8)
        {
            ballForward.speed = 0;
            transform.Translate(Vector3.back * 0.11f * moveSpeedAdd * MoveSpeed, Space.Self);
        }
        else
        {
            ballForward.speed = 0;
            transform.Translate(Vector3.left * 0.05f * moveSpeedAdd * MoveSpeed, Space.Self);
        }
    }

    void AutoRotate(float rotateSpeed)
    {
        raw_rotation = transform.rotation;       //获取当前角度
        transform.LookAt(PlayerPos);               //一瞬间扭到目标角度
        lookat_rotation = transform.rotation;  //获取目标角度
        transform.rotation = raw_rotation;      //回来
        //rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);
        transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, rotateSpeed * Time.fixedDeltaTime);
    }

    void MeleeDmg()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 10.5f)
        {
            Player.GetComponent<Health>().TakeDamage(MeleeDamage * meleeDmgAdd, gameObject);
        }
    }
}
