using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyFSM fsm { get; private set; }

    // set in editor
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fsm = new EnemyFSM(this);
        fsm.Reset(fsm.idleState);

        navIdx = Random.Range(0, 2);
        lm = LayerMask.GetMask("Player", "Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackStart)
        {
            // attack code goes here l8r
            attackStart = false;
            attackDone = true;
        }
        fsm.Update();   
    }

    // FixedUpdate is used for physics updates
    void FixedUpdate()
    {
        calcNav();
        fsm.FixedUpdate();

        transform.Translate(goalVelocity * Time.fixedDeltaTime, Space.World);
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
}
