using System.Runtime.CompilerServices;
using System.Xml.XPath;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool shotByPlayer;
    public float bulletDmg;
    public float bulletSpeed;
    public float initRad;

    private int maxColliders;
    private LayerMask canBeHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // exclude collision check layers
        canBeHit = (shotByPlayer) ? LayerMask.GetMask("Terrain", "Enemies") : LayerMask.GetMask("Terrain", "Player");

        // change scale based on statman
        float scale = initRad + StatMan.sm.bulletRadius;
        transform.localScale = new Vector3(scale, scale, scale);

        maxColliders = 5;
    }

    private void FixedUpdate()
    {
        // continue moving in direction
        //float speed = (StatMan.sm.bulletSpeed < 0.1f) ? 60 : StatMan.sm.bulletSpeed;
        transform.Translate(0f, 0f, bulletSpeed * Time.fixedDeltaTime);

        // raycast check for collisions
        Collider[] results = new Collider[maxColliders];
        int numHits = Physics.OverlapSphereNonAlloc(transform.position, (initRad + StatMan.sm.bulletRadius) / 2, results, canBeHit);

        if (numHits > 0)
        {
            Debug.Log("something was hit");
            // damage enemies, players
            for (int i = 0; i < numHits; i++)
            {
                if (results[i].gameObject.layer == 6 || results[i].gameObject.layer == 8) // hit player or enemy
                {
                    results[i].GetComponent<Health>().damage(bulletDmg);
                }

                if (results[i].gameObject.layer == 6) // player layer
                {
                    Debug.Log("hit player");
                }
                else if (results[i].gameObject.layer == 8) // enemies layer
                {
                    // damage enemy
                    Debug.Log("hit enemy");
                }
                else
                {
                    Debug.Log("hit terrain");
                }
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
