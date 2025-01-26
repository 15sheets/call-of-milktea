using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextVars : MonoBehaviour
{
    public GameObject teasText;
    public GameObject enemiesText;
    public GameObject moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var StatMan sm = GameObject.Find("StatMan");               
        teasText.text = "Teas Drank: " + sm.numTeasDrank.ToString();
        enemiesText.text = "Enemies Defeated: " + sm.enemiesKilled.ToString();
        moneyText.text = "Money Obtained: " + sm.totalMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}