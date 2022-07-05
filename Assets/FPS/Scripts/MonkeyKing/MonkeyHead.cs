using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyHead : MonoBehaviour
{
    private GameObject Player;
    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    private float rotate_angle;
    private Vector3 PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerPos = Player.transform.position;
        PlayerPos.y = transform.position.y;
        AutoRotate(5f);
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
}
