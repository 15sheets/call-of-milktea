using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initMoveSpeed;

    [SerializeField] 
    private Camera cam;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatMan.sm.setPlayer(transform);
        StatMan.sm.incPlayerSpeed(initMoveSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 forwardMovement = camForward * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = cam.transform.right * Input.GetAxis("Horizontal");

        // maybe come back and change this to rb physics stuff later; once map exists and stuff.
        float pspeed = StatMan.sm.playerSpeed; // statman.sm.maxplayerspeed
        Vector3 dxn = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        transform.Translate(dxn * pspeed * Time.fixedDeltaTime, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
