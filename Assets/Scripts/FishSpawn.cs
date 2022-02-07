using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public UIControl UICtrlRef;
    public GameObject fish;
    public GameObject collidedObject = null;
    [SerializeField]
    private GameObject[] currentFish = null;
    public bool spawning;
    public float spawnRate = 5f;
    public float spawnTimer = 3f;
    public float randY;
    float spawnRightX = 3f;
    float spawnLeftX = -3f;
    public bool right;
    Vector2 spawnLocation;

    void spawnFish()
    {
        //spawns once per timer cycle
        if(Time.time > spawnTimer)
        {
            //picks a Y value for the spawned fish between a range that covers the water in teh game screen
            randY = Random.Range(-3.7f, 1f);

            //picks which side of the screen the fish will spawn
            if(Random.Range(0,2) == 1)
            {
                right = true;
            }else{
                right = false;
            }

            //sets spawn location for fish with above variables adn instantiates
            if(right)
            {
                spawnLocation = new Vector2 (spawnRightX, randY);
            }else{
                spawnLocation = new Vector2 (spawnLeftX, randY);
            }
            var spawnedFish = Instantiate(fish, spawnLocation, Quaternion.identity);
            //parents instantiated fish to spawner object to make it easy to find in inspector
            spawnedFish.transform.SetParent(gameObject.transform);

            spawnTimer = Time.time + spawnRate;
        }
    }

    public void killAllFish()
    {
        //finds all the fish, destroys them all

        //fills a gameobject array with all the fish
        currentFish = GameObject.FindGameObjectsWithTag("Fish");

        //cycles for each fish, decimating them individually in milliseconds
        foreach (GameObject _fish in currentFish)
        {
            Destroy(_fish);
        }

        //empties the array
        currentFish = null;
    }

    public void startSpawning()
    {
        //starts the spawning method from UI button with a delay of 3 seconds
        Invoke("fishSpawning", 3f);
    }

    void fishSpawning()
    {
        spawning = true;
    }

    public void stopSpawning()
    {
        //stops spawning and cleans all fish once they are offscreen (2 seconds)
        spawning = false;
        Invoke("killAllFish", 2f);
    }

    public void stopSpawningInstant()
    {
        //used for the tutorial to avoid cheating with pre-existing fish in the middle of the screen
        spawning = false;
        Invoke("killAllFish", 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentFish = null;
        spawning = false;
        spawnTimer = 3f;
        right = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawning)
        {
            //spawns if spawning bool is true
            spawnFish();
        }
    }
}
