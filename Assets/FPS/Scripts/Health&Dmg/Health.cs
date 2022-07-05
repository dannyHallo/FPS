using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Tooltip("Maximum amount of health")]
    public float maxHealth = 10f;
    [Tooltip("Health ratio at which the critical health vignette starts appearing")]
    public float criticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> onDamaged;
    public UnityAction<float> onHealed;
    public UnityAction onDie;
    public GameObject Door1;
    public GameObject Door2;

    
    public float currentHealth { get; set; }
    public bool invincible = false;
    public bool canPickup() => currentHealth < maxHealth;

    public float getRatio() => currentHealth / maxHealth;
    public bool isCritical() => getRatio() <= criticalHealthRatio;

    bool m_IsDead;

    private void Start()
    {    
        currentHealth = maxHealth;
 
    }

    public void Heal(float healAmount)
    {
        float healthBefore = currentHealth;
        currentHealth += healAmount;
        
        //return currentHealth,as long as it's between no.2 and no.3
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // call OnHeal action
        float trueHealAmount = currentHealth - healthBefore;
        if (trueHealAmount > 0f && onHealed != null)
        {
            onHealed.Invoke(trueHealAmount);
        }
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (invincible)
            return;

        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        //print(currentHealth);
        if(currentHealth <= 0)
        {
            if (gameObject.tag == "Rocket")
            {
                GetComponent<Rocket>().DestroyRkt();
            }
            else 
            {
                if (gameObject.name == "MonkeyKing")
                {
                    Door1.GetComponent<Door1>().Open();
                    Door2.GetComponent<Door2>().Open();
                }
                if (gameObject.name == "Player")
                {
                    SceneManager.LoadScene("LoseScene");
                }
                if (gameObject.name == "MonkeyElderExtreme")
                {
                    SceneManager.LoadScene("WinScene");
                }
                Destroy(gameObject);

            }
            

            
            

            
            
        }
        // call OnDamage action
        float trueDamageAmount = healthBefore - currentHealth;
        if (trueDamageAmount > 0f && onDamaged != null)
        {
            onDamaged.Invoke(trueDamageAmount, damageSource);
        }

        HandleDeath();
    }

    public void Kill()
    {
        currentHealth = 0f;
        // call OnDamage action
        if (onDamaged != null)
        {
            onDamaged.Invoke(maxHealth, null);
        }

        HandleDeath();
    }

    private void HandleDeath()
    {
        if (m_IsDead)
            return;

        // call OnDie action
        if (currentHealth <= 0f)
        {
            if (onDie != null)
            {
                
                m_IsDead = true;
                onDie.Invoke();
            }
        }
    }


}
