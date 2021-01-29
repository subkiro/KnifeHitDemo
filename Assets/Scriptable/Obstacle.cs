using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour {

    public string id;
    public string name;
    private GameObject VFX_Explosion;
    private Rigidbody2D rb;


    private void Awake()
    {
        DeactivateCollision(1, false);
    }
    public void InitVisuals(string id,string name, GameObject vfx) {
        this.VFX_Explosion = vfx;
        this.id = id;
        this.name = name;
        rb = GetComponent<Rigidbody2D>();
      
    }

    public void InitVisuals(string id, string name)
    {
       
        this.id = id;
        this.name = name;


    }

    

    public void DestroySelf()
    {
        Instantiate(VFX_Explosion, this.transform.position+new Vector3(0,0,-1),Quaternion.identity,transform.root);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Knife" && this.transform.tag!="Knife") {
            EventManagerController.instance.BonusHit();
            DestroySelf();
        }
    }

    public void DestroyAfterDelay(float delay)
    {
        Destroy(this.gameObject, delay);
    }
    public void DestroyEvent()
    {
        transform.SetParent(null);

        DeactivateCollision(1, false);
        this.rb.AddTorque(Random.Range(-360, 360) * 0.01f, ForceMode2D.Impulse);
        this.rb.AddForce(new Vector2(Random.Range(0, 1), Random.Range(0, 1)) * 10f, ForceMode2D.Impulse);
        this.rb.AddForce((transform.position - WoodCenterController.instance.transform.position).normalized * 200);
        this.DestroyAfterDelay(3f);

    }


    public void DeactivateCollision(float gravity, bool kinematic, float lDrag = 0f, float ADrag = 0.05f, float mass = 1f, bool collision = false)
    {
            if (rb == null) { return; }
            rb.mass = mass;
            rb.drag = lDrag;
            rb.angularDrag = lDrag;
            rb.gravityScale = gravity;
            rb.isKinematic = kinematic;
            rb.drag = 1;

            CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D collider in colliders)
            {
                collider.enabled = collision;
            }


    }
    private void OnEnable()
    {
        EventManagerController.instance.WoodBrokeAction += DestroyEvent;
    }

    private void OnDisable()
    {
        EventManagerController.instance.WoodBrokeAction -= DestroyEvent;
    }
}
