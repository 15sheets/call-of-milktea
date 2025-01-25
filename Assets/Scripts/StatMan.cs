using System.Threading;
using UnityEngine;

public class StatMan : MonoBehaviour
{
    // statman is for getting useful values between places ONLY. NO calculations or updates should happen here except for timer

    public static StatMan sm { get; private set; }

    public float timer { get; private set; } // still deciding where to do calcs and set this up ?

    public int numTeasDrank;
    public int totalMoney;
    public int enemiesKilled;

    public float playerSpeed { get; private set; }
    public float playerMaxHealth { get; private set; }
    public float bulletSpeed { get; private set; }
    public float bulletRadius { get; private set; }
    public float bulletCooldown { get; private set; }

    private Transform player;
    private bool pause;

    void Awake()
    {
        if (sm != null && sm != this) { Destroy(this); }
        sm = this; 
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!pause) { timer += Time.deltaTime; }
    }

    public void decBulletCooldown(float subamt, bool set=false)
    {
        bulletCooldown = (set) ? subamt : bulletCooldown - subamt;
    }

    public void incBulletRadius(float multamt, bool set=false)
    {
        bulletRadius = (set) ? multamt : bulletRadius * multamt;
    }

    public void incBulletSpeed(float multamt, bool set=false)
    {
        bulletSpeed = (set) ? multamt : bulletSpeed * multamt;
    }

    public void incMaxHealth(float addamt)
    {
        playerMaxHealth += addamt;
    }

    public void incPlayerSpeed(float addamt)
    {
        playerSpeed += addamt;
    }

    public void setPlayer(Transform p)
    {
        player = p;
    }

    public Vector3 getPlayerPosition()
    {
        return player.position;
    }

}
