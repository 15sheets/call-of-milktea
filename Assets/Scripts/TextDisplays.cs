using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextDisplays : MonoBehaviour
{
    public bool money;
    public bool ammo;
    public bool hp;

    public TextMeshProUGUI tmp;
    //public TextMeshPro tmp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (money)
        {
            tmp.text = "$" + StatMan.sm.money.ToString();
        } else if (ammo)
        {
            tmp.text = StatMan.sm.ammo.ToString();
        } else if (hp)
        {
            tmp.text = "HP: " + StatMan.sm.player_hp.hp.ToString();
        }
    }
}
