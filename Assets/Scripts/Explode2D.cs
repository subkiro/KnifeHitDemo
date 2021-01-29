using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode2D : MonoBehaviour
{

    public float minForce;
    public float maxForce;
    public float radius;
    public Vector3 offcet;


    private void Start()
    {
        Explode();
    }
    public void Explode()
    {
        foreach (Transform t in transform)
        {
            Rigidbody2D rb = t.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce((rb.transform.position - this.transform.position).normalized * 200);
                 
            }


        }
        Destroy(this.gameObject, 3f);
    }
}


