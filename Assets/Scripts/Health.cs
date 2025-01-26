using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public bool isPlayer;
    public float hp;

    public UnityEvent death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isPlayer)
        {
            StatMan.sm.incMaxHealth(hp);
        }

        if (death == null)
        {
            death = new UnityEvent();
        }
    }

    public void damage(float dmgamt)
    {
        hp -= dmgamt;
        if (hp <= 0f)
        {
            death.Invoke();
        }
    }
    
    public void heal()
    {
        if (isPlayer)
        {
            hp = StatMan.sm.playerMaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
