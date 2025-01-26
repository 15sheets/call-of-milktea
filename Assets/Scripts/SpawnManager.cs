using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float maxSpawnPeriod=40;
    public float minSpawnPeriod=20;
    public float sp_decdelta=60;
    public float spawnPeriod;

    public int maxGroupLimit=5;
    public int minGroupLimit=2;
    public float grp_incdelta=120;
    public int groupLimit;

    private float spdt;
    private float grpdt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPeriod = maxSpawnPeriod;
        groupLimit = minGroupLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (StatMan.sm.timer - spdt > sp_decdelta)
        {
            spdt = StatMan.sm.timer;
            spawnPeriod = Mathf.Max(minSpawnPeriod, spawnPeriod - 5);
        }
        if (StatMan.sm.timer - grpdt > grp_incdelta)
        {
            grpdt = StatMan.sm.timer;
            groupLimit = Mathf.Min(maxGroupLimit, groupLimit + 1);
        }
    }
}
