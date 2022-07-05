using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStand : MonoBehaviour
{
    private Transform m_transform;
    private Transform player_transform;
    private float verticalBobFrequency = 1.2f;
    private float bobbingAmount = 0.8f;
    private Vector3 m_StartPosition;

    private int t = 150;
    private int i = 0;
    public float healAmount = 10f;

    public Health health { get; private set; }

    void Awake()
    {
        // find the health component either at the same level, or higher in the hierarchy
        health = GameObject.Find("Player").GetComponentInParent<Health>();
        if (!health)
        {
            health = GameObject.Find("Player").GetComponentInParent<Health>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();
        player_transform = GameObject.Find("Player").transform;
        m_StartPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPlayerAndHeal();
        // float bobbingAnimationPhase = ((Mathf.Sin(Time.time * verticalBobFrequency) * 0.5f) + 0.5f) * bobbingAmount;
        // transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;
        transform.position = m_StartPosition + Vector3.up * 0;
    }

    void CheckPlayerAndHeal()
    {
        float distance = Vector3.Distance(transform.position, player_transform.position);
        if (distance <= 3.5f)
        {
            health.Heal(healAmount);
        }
    }
}
