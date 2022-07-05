using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTurretController : MonoBehaviour
{
    private GameObject Player;
    //private GameObject Self;
    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    private bool FireCommand = false;
    private int t = 0;
    private int BulletSpeed = 100;
    private int life = 100;
    private float rotate_angle;
    public Transform FirePos;

    [Header("Projectile")]
    public GameObject Shell;


    [Header("Firing Rate")]
    [Range(0f, 10f)]
    public float rate = 10f;

    [Header("Rotate Speed")]
    [Range(0f, 10f)]
    public float Angularspeed = 7f;


    [Header("Sounds")]
    [Tooltip("Sound played when recieving damages")]
    public AudioClip damageTick;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //FirePos = GameObject.Find("FirePos").transform;
    }

    void FixedUpdate()
    {
        float final_rate = 105 - 10 * rate;
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 25f)
        {
            RotateInit();
            transform.rotation = Quaternion.Lerp(raw_rotation, lookat_rotation, Angularspeed * Time.fixedDeltaTime);
            if (rotate_angle <= 5f && t == 0)
            {
                FireCommand = true;
                CheckFire();
                t++;
            }
            else if (t > 0 && t < final_rate)
            {
                t++;
            }

            else
            {
                t = 0;
                FireCommand = false;

            }
        }
        else
        {
            t = 0;
        }
        

    }

    

    private void RotateInit()
    {
        Vector3 target = Player.transform.position;
        raw_rotation = transform.rotation;       //获取当前角度
        transform.LookAt(target);               //一瞬间扭到目标角度
        lookat_rotation = transform.rotation;  //获取目标角度
        transform.rotation = raw_rotation;      //回来
        rotate_angle = Quaternion.Angle(raw_rotation, lookat_rotation);

    }

    private void CheckFire()
    {
        if (FireCommand)
        {
            AudioUtility.CreateSFX(damageTick, transform.position, AudioUtility.AudioGroups.DamageTick, 0f);
            GameObject go = GameObject.Instantiate(Shell, FirePos.position, FirePos.rotation);
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * (BulletSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ("Bullet" == collision.collider.tag)
        {
            life -= 10;
        }
        if (life <= 0)
        {
            GameObject.Destroy(this);
        }

    }
}
