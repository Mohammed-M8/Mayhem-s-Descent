using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour
{
    Animator a;
    public GameObject projectile;
    public GameObject slashPrefab;
    public float projectileSpeed;
    private Aim aim;
    public Transform spawn;
    public Transform slashSpawnPoint;
    public float slashCoolDown = 0.4f;
    public float shootCoolDown = 0.5f;
     bool canShoot = true;
    bool canSlash = true;
    public GameObject image1;
    public GameObject image2;
    public AbilityCooldownUI shootCooldownUI;
    public bool isPaused = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = GetComponent<Animator>();
        aim = GetComponent<Aim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;

        if (Input.GetMouseButtonDown(0)&&canSlash)
        {

            StartCoroutine(slashTimer());
        }

        if (Input.GetMouseButtonDown(1)&&canShoot)
        {
            StartCoroutine(shootTimer());
        }
       
    }

    void PerformSlash()
    {
        Vector3 v = aim.GetMousePosition();
        Vector3 direction = (v - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
        a.SetTrigger("Attack");
        Vector3 spawnPos = slashSpawnPoint.position + transform.forward * 2.0f;
        spawnPos.y = 0.5f;
        GameObject slash = Instantiate(slashPrefab, spawnPos, transform.rotation);
        GameObject.Destroy(slash, 0.4f);
    }

    void Shoot()
    {
        Vector3 spawnPos = spawn.position;

        Vector3 v = aim.GetMousePosition();
    
            
            spawnPos.y = 0.5f;
            GameObject Projectile = Instantiate(projectile, spawnPos, Quaternion.identity);
            Rigidbody rb = Projectile.GetComponent<Rigidbody>();

            Vector3 direction = (v - spawn.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        Projectile.transform.rotation = Quaternion.LookRotation(direction);
            
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

        ElementalEffect effectScript = Projectile.GetComponent<ElementalEffect>();
        if (effectScript != null)
        {
            ElementalEffect.StatusEffect currentAbility = effectScript.effect;

            AbilityIconManager iconManager = FindObjectOfType<AbilityIconManager>();
            if (iconManager != null)
            {
                iconManager.SetAbilityIcon(currentAbility);
            }
        }

        GameObject.Destroy(Projectile, 3f);
        

    }

    IEnumerator shootTimer()
    {
        canShoot = false;
        if (image2 != null) image2.SetActive(false);

        if (shootCooldownUI != null)
            shootCooldownUI.TriggerCooldown(shootCoolDown); //show cooldown

        Shoot();

        yield return new WaitForSeconds(shootCoolDown);

        if (image2 != null) image2.SetActive(true);
        canShoot = true;
    }

    IEnumerator slashTimer()
    {
        canSlash = false;
        if (image1 != null) image1.SetActive(false);

        PerformSlash();

        yield return new WaitForSeconds(slashCoolDown);

        if (image1 != null) image1.SetActive(true);
        canSlash = true;
    }
}
