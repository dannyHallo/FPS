using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp1 : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray1 = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray1.origin, ray1.direction * 30, Color.green);
    }
}
