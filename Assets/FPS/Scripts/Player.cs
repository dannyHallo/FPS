using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rigidbody;
    private GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = GameManager.transform.rotation;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 0.26f, Space.Self);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 0.26f, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 0.16f, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 0.16f, Space.Self);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * 10);
        }
    }
}
