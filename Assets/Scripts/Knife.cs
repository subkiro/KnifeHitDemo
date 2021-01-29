using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Knife : MonoBehaviour
{
    private bool isAbaliable = true;
    public bool isOnTheWood = false;
    private Rigidbody2D rb;
    private CircleCollider2D collider;
    private void Start()
    {
        Init();
    }
    public void Throw() {
        if (isAbaliable) {
            SoundManager.instance.PlayVFX("ThowSound1");
            collider.enabled = true;
            // rb.AddForce(new Vector2(0, 1)*20f, ForceMode2D.Impulse);
            float posYToMove = WoodCenterController.instance.transform.position.y - WoodCenterController.instance.wood.fieldOfImpact;
            this.transform.DOMoveY(posYToMove, .1f).SetEase(Ease.InSine).SetId("Knife").OnComplete(()=>WoodCenterController.instance.wood.OnFinishHit(this));
            this.isAbaliable = false;

            DeactivateCollision(0, true, 0, 0, 1, true);


        }
       
    }

    public void Init()
    {
        this.rb = GetComponent<Rigidbody2D>();
        collider = this.GetComponentInChildren<CircleCollider2D>();
        collider.enabled = false;
        this.transform.DOLocalMoveY(0, .1f).From(-2);
    }

    public void DeactivateCollision( float gravity,bool kinematic,float lDrag = 0f,float ADrag = 0.05f, float mass = 1f,bool collision = false) {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
      

        if (collision.transform.tag == "Knife" && !isOnTheWood )
        {

            this.transform.DOKill();

            EventManagerController.instance.Lost();
            SoundManager.instance.PlayVFX("KnifeHit1");
            KnifeController.instance.DecreaseKnifes();

            DeactivateCollision(1, false, 1, 0.05f, 1, false);
            
            rb.AddForce(Vector2.down*10);
            collider.enabled = false;
            this.rb.gravityScale = 1;
            this.rb.AddTorque(Random.Range(-360, 360) * 0.1f, ForceMode2D.Impulse);

        }
    }


    public void DestroyEvent() {
       transform.SetParent(null);

       DeactivateCollision(1, false);
       this.rb.AddTorque(Random.Range(-360,360)*0.01f, ForceMode2D.Impulse);
       this.rb.AddForce(new Vector2(Random.Range(0, 1), Random.Range(0, 1)) * 10f, ForceMode2D.Impulse);
       this.rb.AddForce((transform.position - WoodCenterController.instance.transform.position).normalized * 200);

       GetComponent<Knife>().DestroyAfterDelay(3f);
       isOnTheWood = false;

    }

   
    public void DestroyAfterDelay(float delay) {
     
        Destroy(this.gameObject, delay);
    }
    public void DestroyImidiate()
    {
            Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        EventManagerController.instance.RoundFinishedAction += DestroyEvent;
        EventManagerController.instance.ClearAllTrushAction += DestroyImidiate;
    }

    private void OnDisable()
    {
        EventManagerController.instance.RoundFinishedAction -= DestroyEvent;
        EventManagerController.instance.ClearAllTrushAction -= DestroyImidiate;
    }
}
