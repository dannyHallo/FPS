using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    public GameObject Lift;
    public GameObject MonkeyKing;
    public AudioClip DoorNoice;

    private Animator AnimLift;

    private float distance;
    private GameObject Player;
    private bool open = false;
    private bool close = false;
    private bool openFlag = false;
    private bool forOnce = true;
    private bool closeForOnce = true;
    private float yRaw;
    private float yNow;
    private float kingDistance;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        AnimLift = Lift.GetComponent<Animator>();
        yRaw = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);
        if(MonkeyKing != null)
        {
            kingDistance = MonkeyKing.GetComponent<MonkeyKingScript>().distance;
        }
        
        //print(kingDistance);
        //print(open);
        yNow = transform.position.y;
        float deltaY = yNow - yRaw;
        //print(deltaY);
        if (distance <= 20 && forOnce)
        {
            open = true;
            AnimLift.SetBool("Lift", true);
            forOnce = false;
        }
        if (openFlag)
        {
            open = true;
        }
        else if(kingDistance <= 80 && kingDistance > 0)
        {
            close = true;
        }

        if (open)
        {
            OpenDoor();
            if (deltaY >= 20)
            {
                open = false;
            }
        }


        if (close)
        {
            open = false;
            if(deltaY < 0)
            {
                if (closeForOnce)
                {
                    AudioSource.PlayClipAtPoint(DoorNoice, transform.position);
                    closeForOnce = false;
                }
                close = false;
            }
            if(deltaY > 0)
            {
                CloseDoor();

            }
        }
    }

    public void Open()
    {
        openFlag = true;
    }

    void OpenDoor()
    {
        transform.Translate(Vector3.up * 6f * Time.deltaTime, Space.Self);
    }
    
    void CloseDoor()
    {
        transform.Translate(Vector3.down * 22f * Time.deltaTime, Space.Self);
    }
}
