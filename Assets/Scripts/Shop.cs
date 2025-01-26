using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public UnityEvent purchase;

    public int price;
    public float modifyAmt;

    private bool inside;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (purchase == null)
        {
            purchase = new UnityEvent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in shop");
        inside = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool spacePress = Input.GetKeyDown(KeyCode.Space);
        if (inside && spacePress && StatMan.sm.subMoney(price))
        {
            purchase.Invoke();
            Debug.Log("purchase");
        }
    }
}
