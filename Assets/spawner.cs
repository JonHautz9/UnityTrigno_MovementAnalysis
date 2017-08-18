using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawner : MonoBehaviour {

    //public Transform spawnLocation; //spawnLocation is the Transform of the second chunk
    public GameObject whatToSpawnPrefab; // This is the first chunk which will always remain in game in order to instantiate copies. *could simply be removed and replaced by Tunnel
    public GameObject whatToSpawnClone; // This acts as a reference to the most recent game object to be instantiated.
    public GameObject player;
    private bool destroy = false; // This boolean is used to prevent the first Tunnel chunk from being destroyed.

    
    public GameObject[] Tunnels = new GameObject[10]; //Objects referencing the 10 chunks being "recycled"

    public GameObject Game;

    private int needPos; // Represents the point the player must cross in order to destroy the previous chunk and instantiating the next chunk
    private Vector3 spawnPos;
    private Vector3 dist;
    public int lastTunnel;
    public int count;


    void Start()
    {
        needPos = 1;   //Location of second chunk.
        dist = new Vector3(0,0,-111.296f);  //Length of tunnel chunk.
        count = 1;          //Set so that the second chunk is deleted first
        lastTunnel = 9;
    }

    void Update()
    {
        if(player.transform.position.z < Tunnels[needPos].transform.position.z) //Deletes then spawns tunnel chunk after needPos is passed.
        {
            if (destroy)
                Object.Destroy(Tunnels[(count+9)%10]); //Destroys tunnel that was just passed
            else
                destroy = true;                      //Only takes place after the first needPos pass.

            spawnObject();
            needPos = (needPos+1)%10;               //Sets next position to cross.
        }
        
    }

    void spawnObject()
    {

        whatToSpawnClone = Instantiate(whatToSpawnPrefab, (Tunnels[lastTunnel]).transform.position , Game.transform.rotation) as GameObject;
        whatToSpawnClone.transform.Translate(dist);
        whatToSpawnClone.transform.Rotate(0, 90, 0);
        whatToSpawnClone.transform.parent = GameObject.Find("Tunnels").transform;

        Tunnels[(count+9)%10] = whatToSpawnClone;
        count = (count+1)%10;
        lastTunnel = (lastTunnel+1)%10;

        
    }
}
