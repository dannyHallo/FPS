using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBomb : MonoBehaviour
{
    public float CycleTime = 30f;

    
    public GameObject Bombing;
    public GameObject MonkeyElder;

    public AudioClip ExplodingNoice;

    public Transform SpawnPos1;
    public Transform SpawnPos2;
    public Transform SpawnPos3;


    private bool haveBombed = false;
    private float i = 0;
    private float c = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(transform.position.y < 10f)
        {
            i += Time.deltaTime;
            if (i >= 2)
            {
                //BombOnce();
                BombCycle();
            }

        }
    }

    void BombOnce()
    {
        if (!haveBombed)
        {
            AudioSource.PlayClipAtPoint(ExplodingNoice, transform.position);

            Instantiate(Bombing, transform.position, transform.rotation);
            Instantiate(MonkeyElder, SpawnPos1.position, SpawnPos1.rotation);
            Instantiate(MonkeyElder, SpawnPos2.position, SpawnPos2.rotation);
            Instantiate(MonkeyElder, SpawnPos3.position, SpawnPos3.rotation);
            haveBombed = true;
        }
        Destroy(gameObject);
    }

    void BombCycle()
    {
        if(c == 0)
        {
            AudioSource.PlayClipAtPoint(ExplodingNoice, transform.position);

            Instantiate(MonkeyElder, SpawnPos2.position, SpawnPos2.rotation);
        }
        c += Time.deltaTime;
        if(c >= CycleTime)
        {
            c = 0;

        }

    }
}
