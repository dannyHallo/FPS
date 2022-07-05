using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyKnife : MonoBehaviour
{
    private GameObject Player;
    private float distance;
    private float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if(distance <= 40)
        {
            Spin();
        }
        else
        {
            if(speed > 0)
            {
                speed -= 0.5f;
            }
            else
            {
                speed = 0;
            }
            transform.Rotate(0, speed, 0);

        }
    }

    void Spin()
    {
        if(speed < 50)
        {
            speed += 0.2f;
        }      
        transform.Rotate(0, speed, 0);
        
    }
}

