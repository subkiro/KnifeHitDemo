using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour {

    public string id;
    public string name;
    public GameObject VFX_Explosion;
  


    private void Awake()
    {
       
        
    }
    public void InitVisuals(string id,string name, GameObject vfx) {
        this.VFX_Explosion = vfx;
        this.id = id;
        this.name = name;
      
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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Knife" && this.transform.tag!="Knife" && collision.transform.GetComponent<Knife>()!=null) {
            EventManagerController.instance.BonusHit(this);
            DestroySelf();
        }
    }

    public void DestroyAfterDelay(float delay)
    {
        Destroy(this.gameObject, delay);
    }
    public void DestroyEvent(Object sender)
    {
        transform.SetParent(null);

       
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.AddTorque(Random.Range(-360, 360) * 0.01f, ForceMode2D.Impulse);
            rb.AddForce(new Vector2(Random.Range(0, 1), Random.Range(0, 1)) * 10f, ForceMode2D.Impulse);
            rb.AddForce((transform.position - WoodCenterController.instance.transform.position).normalized * 200);


            DeactivateCollision(false);

            this.DestroyAfterDelay(3f);
       

    }


    public void DeactivateCollision(bool collision = false)
    {
           
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D collider in colliders)
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
