using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyStare : MonoBehaviour
{
    private GameObject Player;
    private Vector3 PlayerPos;
    private Quaternion raw_rotation;
    private Quaternion lookat_rotation;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = Player.transform.position;
        AutoRotate(0.5f);
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
}
