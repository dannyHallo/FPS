using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{

    public float SelfDestroyTime = 45;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckDestroyTime();
    }

    void CheckDestroyTime()
    {
        SelfDestroyTime -= Time.deltaTime;
        if (SelfDestroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

}
