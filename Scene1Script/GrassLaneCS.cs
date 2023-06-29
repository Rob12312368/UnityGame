using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassLaneCS : MonoBehaviour {
    
    public Transform laneObj;
    public Transform testBarrierObj;
    public Transform SphereBarrierObj;
    public Transform ChrismasTreeBarrierObj;
    public Transform coinObj;
    public Transform buffShield;
    public Transform specialCoin2;
    private Vector3 nextLaneSpawn;

    private const float laneSpawnDistance = 7.5f; // distance between lanes
    private const int numberOfInitialLnaes = 15;
    private const float laneSpawnTimeInterval = 0.2f;
    private const int coinBatchSize = 5; // generate five coin a time
    private int empytLaneCount = 0;

    private bool coinGenerating; // true if last batch of coin generation haven't finished
    private bool specialGenerating;


    private bool debugMode = false; // debug set true to mark barrier and block
    private int degbugcnt = 0;
    

	// Use this for initialization
	void Start () {

        coinGenerating = false;
        specialGenerating = false;

        // there is two non-instantiate lanes at map
        nextLaneSpawn = new Vector3(0, 0, 2*laneSpawnDistance);

        // create initial lane before game start
        for(int i=0; i<numberOfInitialLnaes-2; i++)
        {
            Instantiate(laneObj, nextLaneSpawn, laneObj.rotation);
            nextLaneSpawn += new Vector3(0, 0, laneSpawnDistance);
        }
        
        StartCoroutine(spawnLane());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator spawnLane()
    {
        yield return new WaitForSeconds(laneSpawnTimeInterval);
        GameObject cloneLane = (GameObject)Instantiate(laneObj, nextLaneSpawn, laneObj.rotation).gameObject;
        nextLaneSpawn.z += laneSpawnDistance;
        StartCoroutine(spawnLane());

        // generate coin 20%
        if(!coinGenerating && Random.Range(0f, 1f) < 0.2f)
        {
            coinGenerating = true;
        }
        // generate specialCoin 10%
        else if (!coinGenerating && Random.Range(0f, 1f) < 0.1f)
        {
            specialGenerating = true;
        }

        Transform barrierObject = null;
        // generate a Barrier with probability of 0.4
        if (Random.Range(0f, 1f)<0.4f)
        {
            // 20% ChristmasTree barrier
            // 10% left barrier 
            // 10% right barrier
            // 20% middel barrier
            // 20% sinx barrier
            // 20% empty lane
            float barrierSeed = Random.Range(0f, 1f);

            
            if(barrierSeed < 0.2f) // chirstmas tree
            {
                // for all lanes
                for (int i = -2; i < 3; i++)
                {
                    if (Random.Range(0f, 1f) > 0.5f)
                    {
                        barrierObject = (Transform)Instantiate(ChrismasTreeBarrierObj, nextLaneSpawn + new Vector3(2*i, -nextLaneSpawn.y, -laneSpawnDistance), laneObj.rotation);
                    }
                }
            }
            else if(barrierSeed < 0.3f) // left barreier
            {
                barrierObject = (Transform)Instantiate(testBarrierObj, nextLaneSpawn + new Vector3(2.88f,1f, -laneSpawnDistance), laneObj.rotation);
                coinGenerationFunction(1);
            }
            else if(barrierSeed < 0.40f) // right barrier
            {
                barrierObject = (Transform)Instantiate(testBarrierObj, nextLaneSpawn + new Vector3(-2.88f, 1f, -laneSpawnDistance), laneObj.rotation);
                coinGenerationFunction(2);
            }
            else if(barrierSeed < 0.60f) // middle barrier
            {
                barrierObject = (Transform)Instantiate(testBarrierObj, nextLaneSpawn + new Vector3(0, 1f, -laneSpawnDistance), laneObj.rotation);
                coinGenerationFunction(3);
            }
            else if(barrierSeed < 0.80f) // sinx barrier
            {
                Instantiate(SphereBarrierObj, nextLaneSpawn + new Vector3(0, 1f, -laneSpawnDistance), laneObj.rotation);
                coinGenerationFunction(0);
            }
            else // gap
            {
                // at most two empty lane
                if (empytLaneCount < 2)
                {
                    Destroy(cloneLane, 0f);
                    empytLaneCount++;
                }
            }
        }
        // no Barrier is generated
        else
        {
            // only pure lane will clear empty lane count
            empytLaneCount = 0;

            if (debugMode && coinGenerating)
            {
                cloneLane.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            }

            coinGenerationFunction(0);
        }

        if (debugMode)
        {
            if(degbugcnt%2 == 0)
            {
                barrierObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                cloneLane.GetComponentInChildren<Renderer>().material.color = Color.red;
                degbugcnt++;
            }
            else
            {
                barrierObject.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                cloneLane.GetComponentInChildren<Renderer>().material.color = Color.blue;
                degbugcnt++;
            }
        }

    }

    // typeIndex
    // 0: Lane
    // 1: Right Barrier
    // 2: Left Barrier
    // 3: Middle Barrier
    private void coinGenerationFunction(int typeIndex)
    {
        if (coinGenerating)
        {
            switch (typeIndex)
            {
                case 0: 
                    int coinRndPositionX = 2 * Random.Range((int)-2, (int)2);
                    for (int i = -2; i < coinBatchSize-2; i++)
                    {
                        Instantiate(coinObj, nextLaneSpawn + new Vector3(coinRndPositionX, 1f, -laneSpawnDistance+i), coinObj.rotation);
                        //Instantiate(specialCoin, nextLaneSpawn + new Vector3(coinRndPositionX, 1f, -laneSpawnDistance + i), coinObj.rotation);
                        
                    }
                    break;
                case 1:
                    for ( float i = -2.3f; i < coinBatchSize - 2; i+=1.2f)
                    {
                        Instantiate(coinObj, nextLaneSpawn + new Vector3(-2f, 1f, -laneSpawnDistance + i), coinObj.rotation);
                    }
                    break;
                case 2:
                    for (float i = -2.3f; i < coinBatchSize - 2; i += 1.2f)
                    {
                        Instantiate(coinObj, nextLaneSpawn + new Vector3(2f, 1f, -laneSpawnDistance + i), coinObj.rotation);
                    }
                    break;
                case 3:
                    int coinRndPositionXm = 2 * Random.Range((int)-2, (int)2);
                    coinRndPositionXm = coinRndPositionXm > 0 ? 4 : -4;
                    for (float i = -2.3f; i < coinBatchSize - 2; i += 1.2f)
                    {
                        Instantiate(coinObj, nextLaneSpawn + new Vector3(coinRndPositionXm, 1f, -laneSpawnDistance + i), coinObj.rotation);
                    }
                    break;

            }
            
            coinGenerating = false;
        }
        else if (specialGenerating) {

            int coinRndPositionX = 2 * Random.Range((int)-2, (int)2);
            float specialCoinRnd = Random.Range(0f, 1f);
            switch (typeIndex)
            {
                case 0:
                    if(specialCoinRnd<0.5)
                        Instantiate(buffShield, nextLaneSpawn + new Vector3(coinRndPositionX, 1f, -laneSpawnDistance), coinObj.rotation);
                    else
                        Instantiate(specialCoin2, nextLaneSpawn + new Vector3(coinRndPositionX, .5f, -laneSpawnDistance), specialCoin2.rotation);
                    break;
                case 1:
                    if (specialCoinRnd < 0.5)
                        Instantiate(buffShield, nextLaneSpawn + new Vector3(-1f, 1f, -laneSpawnDistance), coinObj.rotation);
                    else
                        Instantiate(specialCoin2, nextLaneSpawn + new Vector3(-1f, .5f, -laneSpawnDistance), specialCoin2.rotation);
                    break;
                case 2:
                    if(specialCoinRnd < 0.5)
                        Instantiate(buffShield, nextLaneSpawn + new Vector3(1f, 1f, -laneSpawnDistance), coinObj.rotation);
                    else
                        Instantiate(specialCoin2, nextLaneSpawn + new Vector3(1f, .5f, -laneSpawnDistance), specialCoin2.rotation);
                    break;
                case 3:
                    int coinRndPositionXm = 2 * Random.Range((int)-2, (int)2);
                    coinRndPositionXm = coinRndPositionXm > 0 ? 4 : -4;
                    if (specialCoinRnd < 0.5)
                        Instantiate(buffShield, nextLaneSpawn + new Vector3(coinRndPositionXm, 1f, -laneSpawnDistance), coinObj.rotation);
                    else
                        Instantiate(specialCoin2, nextLaneSpawn + new Vector3(coinRndPositionXm, .5f, -laneSpawnDistance), specialCoin2.rotation);
                    break;
            }

            specialGenerating = false;
        }
    }
}
