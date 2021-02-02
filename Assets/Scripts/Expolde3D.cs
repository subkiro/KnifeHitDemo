using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expolde3D : MonoBehaviour
{
    public float minForce;
    public float maxForce;
    public float radius;
    public Vector3 offcet;

  
    private void Start()
    {
        Explode();
    }
    public void Explode() {
        transform.Rotate(0, 0, Random.Range(0, 180));
        foreach (Transform t in transform)
        {
            Rigidbody rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position + offcet, radius);
                rb.AddTorque(Vector3.one*Random.Range(-360,360)*0.01f, ForceMode.Impulse);
            }

            
        }
        Destroy(this.gameObject, 3f);
    }
}
