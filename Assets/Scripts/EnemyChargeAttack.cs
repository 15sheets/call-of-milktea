using UnityEngine;

public class EnemyChargeAttack : MonoBehaviour
{
    public float windupTime;
    public float chargeVelocity;
    public float chargeTime;

    public float chargeDmg;
    public float contactDmg;

    public bool attacking;

    private float timer;
    private Vector3 chargeDxn;
    private EnemyBehavior eb;
    private float totalTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eb = GetComponent<EnemyBehavior>();
        totalTime = windupTime + chargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking && timer < windupTime)
        {
            eb.goalVelocity = Vector3.zero;

            timer += Time.deltaTime;
        } 
        else if (attacking && timer < totalTime && chargeDxn.magnitude < .3f)
        {
            chargeDxn = (StatMan.sm.getPlayerPosition() - transform.position).normalized;
            eb.goalVelocity = chargeDxn * chargeVelocity;

            timer += Time.deltaTime;
        } 
        else if (attacking && timer < totalTime)
        {
            eb.goalVelocity = chargeDxn * chargeVelocity;

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

        if (other.gameObject.layer == 6 && attacking)
        {
            int playerSpeak = Random.Range(0, 6);
            if (playerSpeak == 1)
            {
                SoundMan.sm.PlayerHit();
            }
            
            StatMan.sm.damagePlayer(chargeDmg);
        } else if (other.gameObject.layer == 6)
        {
            int playerSpeak = Random.Range(0, 6);
            if (playerSpeak == 1)
            {
                SoundMan.sm.PlayerHit();
            }
            
            StatMan.sm.damagePlayer(contactDmg);
        }
    }

    public void startAttack()
    {
        attacking = true;
        timer = 0;
        chargeDxn = Vector3.zero;
    }
}
