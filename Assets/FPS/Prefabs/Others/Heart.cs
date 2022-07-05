using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    int i = 0;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
;        if (flag)
        {
            i++;
            if(i >= 30)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Destroy()
    {
        flag = true;
        //print(flag);



    }
}
