using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : MonoBehaviour
{
    public float speed = 5.0f;
    public float forwardSpeed = 0.1f;
    public float jumpForce = 10f;
    public int jumpCounter = 0;
    public int jumpMax = 2;
    public int health = 0;
    private Rigidbody rb;
    public GameObject child;
    public float x;

    public Collider[] attackHitboxes;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        rb.angularVelocity = Vector3.zero;

        x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.A))
        {
            child.transform.rotation = new Quaternion(0, 180, 0, 0);
            x *= -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            child.transform.rotation = new Quaternion();
            x *= -1;
        }

        transform.Translate(x, 0, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCounter != jumpMax)
            {
                rb.velocity = Vector3.zero;
                jumpCounter++;
                rb.AddForce(0, jumpForce, 0);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            LaunchAttack(attackHitboxes[0], 150f, 500f, 6);
        }
        if (Input.GetMouseButtonDown(1))
        {
            LaunchAttack(attackHitboxes[1], 0f, -1000.0f, 15);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ground")
        {
            jumpCounter = 0;
        }
    }

    private void LaunchAttack(Collider coll, float d, float t, int h)
    {
        Collider[] cols = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents, coll.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            Debug.Log(c);

            if (c.transform == transform)
            { continue; }

            if (c.transform.position.x < transform.position.x)
            {
                d *= -1;
            }

            if (c.transform.tag == "enemy")
            {
                float i = (c.GetComponentInParent<AI>().health / 100.0f) + 0.5f;
                Rigidbody r = c.GetComponentInParent<Rigidbody>();
                r.velocity = Vector3.zero;
                r.AddForce(d * i, t * i, 0);
                c.GetComponentInParent<AI>().health += h;
            }
        }
    }
}
