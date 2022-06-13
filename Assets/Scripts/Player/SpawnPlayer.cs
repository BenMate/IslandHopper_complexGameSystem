using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    Vector3 defaultSpawnOffset;

    // Start is called before the first frame update
    void Start()
    {
        //for now only moves player to spawn
        defaultSpawnOffset = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);
        player.transform.position = defaultSpawnOffset;


    }


}
