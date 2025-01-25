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
    private Health player_hp;
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

    public void damagePlayer(float dmg)
    {
        Debug.Log("player damaged");
        player_hp.damage(dmg);
    }
    public void healPlayer(float health)
    {
        player_hp.heal(health);
    }

    public void decBulletCooldown(float multamt, bool set=false)
    {
        bulletCooldown = (set) ? multamt : bulletCooldown * multamt;
    }

    public void incBulletRadius(float addamt)
    {
        bulletRadius += addamt;
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
        player_hp = p.GetComponent<Health>();
    }

    public Vector3 getPlayerPosition()
    {
        return player.position;
    }

    public void pauseTimer(bool p)
    {
        pause = p;
    }

}
