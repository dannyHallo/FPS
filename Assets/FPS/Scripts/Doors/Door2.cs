using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    private float distance;
    private GameObject Player;
    private float yRaw;
    private float yNow;
    private bool openFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        yRaw = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (openFlag)
        {
            yNow = transform.position.y;
            if(yNow - yRaw <= 20)
            {
                transform.Translate(Vector3.up * 0.4f, Space.Self);
            }
        }
    }

    public void Open()
    {
        openFlag = true;
    }
}
