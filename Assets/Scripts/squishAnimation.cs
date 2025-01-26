using UnityEngine;

public class squishAnimation : MonoBehaviour
{
    public bool isPlayer;
    public float alotoftea;

    public float minWigglePhase;
    public float maxWigglePhase;
    public float minWiggleAmplitude;
    public float maxWiggleAmplitude;

    private float normal_yscale;
    public float wigglePhase;
    public float wiggleAmplitude;

    private float wiggleTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normal_yscale = transform.localScale.y;
        wigglePhase = (isPlayer) ? minWigglePhase : Random.Range(minWigglePhase, maxWigglePhase);
        wiggleAmplitude = (isPlayer) ? minWiggleAmplitude : Random.Range(minWiggleAmplitude, maxWiggleAmplitude);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            float teapercent = Mathf.Min(1f, StatMan.sm.numTeasDrank / alotoftea);
            wigglePhase = Mathf.Lerp(minWigglePhase, maxWigglePhase, teapercent);
            wiggleAmplitude = Mathf.Lerp(minWiggleAmplitude, maxWiggleAmplitude, teapercent);
        }

        wiggleTimer += Time.deltaTime * wigglePhase;

        float yscale = normal_yscale + wiggleAmplitude * Mathf.Sin(wiggleTimer);
        transform.localScale = new Vector3(transform.localScale.x, yscale, transform.localScale.z);
    }
}
