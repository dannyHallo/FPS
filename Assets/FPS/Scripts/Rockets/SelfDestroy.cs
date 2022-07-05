using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{

    public float SelfDestroyTime = 5;
    public GameObject thisRocket;   
    public GameObject Flame;
    public AudioClip ExplodingNoice;

    private Vector3 pos;
    private float delay = 5f;
    private bool flag1 = true;
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
        if (SelfDestroyTime <= 0 && flag1)
        {
            DestroyRkt();
            flag1 = false;
        }
        if ((SelfDestroyTime + 5) <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyRkt()
    {
        
        BallDestruct bd = thisRocket.GetComponent<BallDestruct>();
        bd.Destroy();
        pos = transform.position;
        Instantiate(Flame, pos, transform.rotation);
        AudioSource.PlayClipAtPoint(ExplodingNoice, pos);
        

    }
}
