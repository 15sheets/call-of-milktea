using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public UnityEvent purchase;

    public int price;
    public string label;

    public Color rich;
    public Color poor;

    [SerializeField] private TextMeshPro tmp;
    private bool inside;
    private Color defCol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (purchase == null)
        {
            purchase = new UnityEvent();
        }
        defCol = tmp.faceColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("in shop");
        inside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inside = false;
    }

    public void increasePrice(int inc)
    {
        price += inc;
    }

    // Update is called once per frame
    void Update()
    {
        bool spacePress = Input.GetKeyDown(KeyCode.Space);
        if (spacePress) { Debug.Log(StatMan.sm.money); }

        if (inside && price <= StatMan.sm.money)
        {
            tmp.faceColor = rich;
        } else if (inside)
        {
            tmp.faceColor = poor;
        } else
        {
            tmp.faceColor = defCol;
        }

        if (inside && spacePress && StatMan.sm.subMoney(price))
        {
            purchase.Invoke();
            Debug.Log("purchase");
        }

        tmp.text = label + "\n$" + price.ToString();

    }
}
