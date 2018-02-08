using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    //public GameObject[] players;
    public GameObject spawn;

    public void SpawnPlayer(GameObject c)
    {
        c.transform.position = gameObject.transform.position;
    }
}
