using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyFSM fsm { get; private set; }

    public UnityEvent attack;

    // set in editor
    public int moneyDropped;
    public float rotationSpeed; // speed enemy rotates to face player
    public float maxNavRange; // range where enemy will try to dodge obstacles
    public float maxChaseRange; // won't follow player if out of this range
    public float maxAttackRange; // range where enemy will start attacking
    public float maxSpeed; // speed that enemy moves at
    
    // calculated here, read only by fsm
    public bool shouldNav; // if needs to dodge obstacles
    public Vector3 navDirection; 
    public bool attackDone;

    // written to by fsm
    public Vector3 goalVelocity;
    public bool attackStart;

    private int navIdx; // 0 for left, 1 for right
    private LayerMask lm;
    private float faceAngle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fsm = new EnemyFSM(this);
        fsm.Reset(fsm.idleState);

        navIdx = Random.Range(0, 2);
        lm = LayerMask.GetMask("Player", "Terrain");
        faceAngle = 0;

        if (attack == null)
        {
            attack = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackStart)
        {
            // attack code goes here l8r
            attackStart = false;

            attack.Invoke();
        }
        fsm.Update();   
    }

    // FixedUpdate is used for physics updates
    void FixedUpdate()
    {
        calcNav();
        fsm.FixedUpdate();

        Vector3 playerdxn = StatMan.sm.getPlayerPosition() - transform.position;
        playerdxn.y = 0;
        float angle = Mathf.Atan2(playerdxn.z, playerdxn.x) * Mathf.Rad2Deg;
        faceAngle = Mathf.LerpAngle(faceAngle, angle, rotationSpeed * Time.fixedDeltaTime);
        
        transform.rotation = Quaternion.Euler(0, -faceAngle, 0);
        transform.Translate(goalVelocity * Time.fixedDeltaTime, Space.World);

        if (faceAngle > 180)
        {
            faceAngle = -faceAngle - 360;
        }
        if (faceAngle < -180)
        {
            faceAngle += 360;
        }
    }

    private void calcNav()
    {
        Vector3 playerdxn = StatMan.sm.getPlayerPosition() - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerdxn, out hit, maxNavRange, lm) && hit.collider.gameObject.layer == 7)
        { // if hit terrain
            shouldNav = true;
            navDirection = Vector3.Cross(playerdxn, Vector3.up).normalized; // left
            if (navIdx != 0)
            {
                navDirection *= -1; // right
            } 
        } else
        {
            shouldNav = false;
        }

        // ray cast in player direction 
        // if intersects with a terrain, should nav
            // nav direction - calc normalized perpendicular (xz axis) vectors 
            // pick one based on navIdx
    }

    public void die()
    {
        // add money to player balance
        StatMan.sm.addMoney(moneyDropped);
        StatMan.sm.enemiesKilled++;
        // play animation (later)
        // destroy self
        Destroy(gameObject);
    }
}
