using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyExtreme : MonoBehaviour
{

    private GameObject Player;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Head;
    public GameObject NukeBomb;
    
    public GameObject Rayway1;
    public GameObject Rayway2;
    public Transform FirePos;
    


    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    private float rotate_angle;
    private float distance;
    private Vector3 PlayerPos;
    private Vector3 PlayerFairPos;
    private bool attackFlag = false;
    private int c = 0;
    private float maxHealth;
    private float health;
    private float lastHealth;
    private int countFlag = 0;
    private Animator Anim;
    private int rate = 200;
    private bool nukeAttack = false;
    private float i = 0;



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

        PlayerPos = Player.transform.position;

        HealthCheck();

        if (lastHealth - health > 0)
        {
            attackFlag = true;
        }
        else if (distance <= 180)
        {
            Attack();
        }
        else if (attackFlag)
        {
            Attack();
            if(distance >= 400)
            {
                attackFlag = false;
            }
        }
        lastHealth = health;
    }

    void Attack()
    {
        AutoRotate(Head, 1.95f);
        //Move();
        distance = Vector3.Distance(Rayway1.transform.position, Player.transform.position);
        if (Physics.Raycast(Rayway1.transform.position, Rayway1.transform.forward, out RaycastHit hitInfo, 1000))
        {
            //print(hitInfo.collider.tag);
            //Debug.DrawLine(Rayway1.transform.position, hitInfo.transform.position);
            if (hitInfo.collider.tag == "Player")
            {
                Player.GetComponent<Health>().TakeDamage(1.25f, gameObject);
            }

        }
        if (Physics.Raycast(Rayway2.transform.position, Rayway2.transform.forward, out RaycastHit hitInfo2, 1000))
        {
            //print(hitInfo2.collider.tag);
            //Debug.DrawLine(Rayway1.transform.position, hitInfo.transform.position);
            if (hitInfo2.collider.tag == "Player")
            {
                Player.GetComponent<Health>().TakeDamage(0.25f, gameObject);
            }

        }

        if (nukeAttack)
        {  
            if(i == 0)
            {
                Instantiate(NukeBomb, FirePos.position, FirePos.rotation);
            }
            i += Time.deltaTime;
            if (i >= 20)
            {
                i = 0;
            }
        }

    }

    void Move()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        
        PlayerFairPos = PlayerPos;
        PlayerFairPos.y = transform.position.y;
        raw_rotation = transform.rotation;       //获取当前角度
        transform.LookAt(PlayerFairPos);               //一瞬间扭到目标角度
        lookat_rotation = transform.rotation;  //获取目标角度
        transform.rotation = raw_rotation;      //回来
        rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);
        transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, 0.5f * Time.fixedDeltaTime);
        
        
        if (distance >= 10)
        {
            transform.Translate(transform.forward * 0.10f);
            //ballForward.speed = 1;
        }
        else if (distance < 8)
        {
            //ballForward.speed = 0;
            //transform.Translate(Vector3.back * 0.11f, Space.Self);
        }
        else
        {
            //ballForward.speed = 0;
            //transform.Translate(Vector3.left * 0.05f, Space.Self);
        }
    }

    void AutoRotate(GameObject obj, float rotateSpeed)
    {
        raw_rotation = obj.transform.rotation;       //获取当前角度
        obj.transform.LookAt(PlayerPos);               //一瞬间扭到目标角度
        lookat_rotation = obj.transform.rotation;  //获取目标角度
        obj.transform.rotation = raw_rotation;      //回来
        rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);
        obj.transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, rotateSpeed * Time.fixedDeltaTime);
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
                Anim.SetBool("Throw", true);

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
                nukeAttack = true;
                Green.SetActive(false);
                Red.SetActive(true);
            }

        }
    }
}
