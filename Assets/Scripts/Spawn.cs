using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    //public GameObject[] players;
    public GameObject spawn;

    void OnTriggerEnter(Collider c)
    {
        print("boink");
        c.transform.position = spawn.transform.position;
    }
}
