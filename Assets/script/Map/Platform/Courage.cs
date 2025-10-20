using UnityEngine;

public class Courage : MonoBehaviour
{
    PlayerController player;

    public GameObject courage;

    bool step = false;
    public float runTime = 20f;

    bool reSpawnOn = false;
    public float reSpawnTime = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("HumanM_Model").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.stepCourage)
        {
            runTime -= Time.deltaTime;
            if(runTime <= 0)
            {
                runTime = 20f;
                courage.SetActive(false);
                reSpawnOn = true;
            }
        }
        if(reSpawnOn)
        {
            reSpawnTime -= Time.deltaTime;
            if (reSpawnTime <= 0)
            {
                reSpawnTime = 10;
                courage.SetActive(true);
                reSpawnOn = false;
                player.stepCourage = false;
            }  
        }
    }
}
