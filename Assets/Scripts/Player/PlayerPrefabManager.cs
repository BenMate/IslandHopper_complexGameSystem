using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabManager : MonoBehaviour
{
    public GameObject player;
    public GameObject SpawnLocation;

    public float yDeathPos = -50;
    void Start()
    {
        //spawn player at spawnlocations pos
        Instantiate(player, SpawnLocation.transform.position, Quaternion.identity, transform);

       
    }  

    private void Update()
    {
        if (player.transform.position.y < yDeathPos)
            player.transform.position = SpawnLocation.transform.position;

    }
}
