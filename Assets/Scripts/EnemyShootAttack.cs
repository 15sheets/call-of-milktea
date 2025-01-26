using UnityEngine;

public class EnemyShootAttack : MonoBehaviour
{
    
    public float windupTime;
    public float shootVelocity;
    public float shootTime;

    public float contactDmg;
    public bool attacking;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;

    private float timer;
    private Vector3 shootDxn;
    private EnemyBehavior eb;
    private float totalTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eb = GetComponent<EnemyBehavior>();
        totalTime = windupTime + shootTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (attacking && timer < windupTime)
        {
            eb.goalVelocity = Vector3.zero;

            timer += Time.deltaTime;
        } 
        else if (attacking && timer < totalTime)
        {
            Aim();
            Shoot();
            timer += Time.deltaTime;
        } 
        else if (attacking)
        {
            attacking = false;
            eb.attackDone = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy hit player");

        if (other.gameObject.layer == 6)
        {
            StatMan.sm.damagePlayer(contactDmg);
        }
    }

    public void startAttack()
    {
        attacking = true;
        timer = 0;
        shootDxn = Vector3.zero;
    }

    private void Aim()
        {
            var shootDxn = (StatMan.sm.getPlayerPosition() - transform.position).normalized;

            // Ignore the height difference.
            shootDxn.y = 0;

            // Make the transform look the direction it's aiming
            transform.forward = shootDxn;
        }

        private void Shoot()
        {
            var projectile = Instantiate(projectilePrefab, transform.Find("BulletSpawnPoint").gameObject.transform.position, Quaternion.identity);
            projectile.transform.forward = transform.forward;

        }
}
