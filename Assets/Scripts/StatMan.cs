using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatMan : MonoBehaviour
{
    // statman is for getting useful values between places ONLY. NO calculations or updates should happen here except for timer

    public static StatMan sm { get; private set; }

    public float timer { get; private set; } // still deciding where to do calcs and set this up ?
    public int money { get; private set; } // current balance
    public int ammo { get; private set; }

    public float totalTime;
    public int numTeasDrank;
    public int totalMoney;
    public int enemiesKilled;

    public float playerSpeed { get; private set; }
    public float playerMaxHealth { get; private set; }
    public float bulletSpeed { get; private set; }
    public float bulletRadius { get; private set; }
    public float bulletCooldown { get; private set; }
    public int maxAmmo { get; private set; }

    private Transform player;
    public Health player_hp;
    private bool pause;

    void Awake()
    {
        if (sm != null && sm != this) { Destroy(this); }
        sm = this; 
        //DontDestroyOnLoad(this);
    }

    private void Update()          
    { 
        // CHANGE THIS TO CHECK AND ONLY INC TIMER IF THE CURRENT SCENE IS THE GAMESCENE
        if (!pause) { timer += Time.deltaTime; }
    }

    public void endGame()
    {
        totalTime = timer;
        timer = 0;
        //SceneManager.LoadScene()
    }

    public bool useAmmo()
    {
        if (ammo > 0)
        {
            ammo -= 1;
            return true;
        }
        return false;
    }

    public void refillAmmo()
    {
        ammo = maxAmmo;
    }

    public void incMaxAmmo(int addamt)
    {
        maxAmmo += addamt;
        ammo = maxAmmo;
    }

    public bool subMoney(int subamt)
    {
        if (money < subamt)
        {
            return false;
        }
        money -= subamt;
        return true;
    }

    public void addMoney(int addamt)
    {
        money += addamt;
        totalMoney += addamt;
    }

    public void damagePlayer(float dmg)
    {
        Debug.Log("player damaged");
        player_hp.damage(dmg);
    }
    public void healPlayer()
    {
        player_hp.heal();
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
        bool hpgot = p.TryGetComponent<Health>(out player_hp);
        //Debug.Log(hpgot);
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
