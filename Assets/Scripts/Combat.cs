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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = GetComponent<Animator>();
        aim = GetComponent<Aim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 v = aim.GetMousePosition();
            Vector3 direction = (v - transform.position).normalized;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            a.SetTrigger("Attack");
            PerformSlash();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
       
    }

    void PerformSlash()
    {
        // Offset the slash position slightly in front of the player
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
            GameObject.Destroy(Projectile, 3f);

        

    }
}
