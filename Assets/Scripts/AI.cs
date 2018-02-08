using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour {

    public Text text;
    private string temp;
    public int health = 0;
    private Rigidbody cont;
    private Vector3 moveVector;
    private float verticalVel;

    public Collider[] attackHitboxes;
    // Use this for initialization
    void Start()
    {
        cont = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        cont.angularVelocity = Vector3.zero;

        text.text = health + "%";
    }

}
