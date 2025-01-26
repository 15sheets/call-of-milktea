using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initMoveSpeed;

    [SerializeField] 
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatMan.sm.setPlayer(transform);
        StatMan.sm.incPlayerSpeed(initMoveSpeed);
        SoundMan.sm.PlayerLaugh();

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

    public void die()
    {
        SoundMan.sm.PlayerDeath();
        StatMan.sm.pauseTimer(true);
        StatMan.sm.endGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
