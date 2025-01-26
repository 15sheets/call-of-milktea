using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeedOffset;
    public Vector3 offset;
    public float followDistance;
    public Quaternion rotation;

    public float minTeleportDistance = 100f;

    [SerializeField]
    private Transform toTrack;

    private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Update()
    {
        moveSpeed = StatMan.sm.playerSpeed - moveSpeedOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, toTrack.position) > minTeleportDistance)
        {
            transform.position = toTrack.position + offset - transform.forward * followDistance;
        } else
        {
            Vector3 pos = Vector3.Lerp(transform.position, toTrack.position + offset - transform.forward * followDistance, moveSpeed * Time.deltaTime);
            transform.position = pos;
            transform.rotation = rotation;
        }
    }
}
