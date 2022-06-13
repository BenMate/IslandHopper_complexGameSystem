using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform spawn;

    public Vector3 defaultSpawnOffset = new Vector3(0, 5, 0);

    // Start is called before the first frame update
    void Start()
    {

        if(spawn != null)       
            Instantiate(player, spawn.transform.position, Quaternion.identity, transform); 
        else       
            Instantiate(player, transform.position + defaultSpawnOffset, Quaternion.identity, transform);
 
    }
}
