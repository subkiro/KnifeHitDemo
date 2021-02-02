using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knife : MonoBehaviour
{
    private bool isAbaliable = true;
    public bool isOnTheWood = false;
    private Rigidbody2D rb;
    public BoxCollider2D colliderBox;
    public PolygonCollider2D polygonCol;
    private void Start()
    {
        Init();
    }
    public void Throw() {
        if (isAbaliable) {
            DeactivateCollision(true, true);
            SoundManager.instance.PlayVFX("ThowSound1");
            float posYToMove = WoodCenterController.instance.transform.position.y - WoodCenterController.instance.fieldOfImpact;
            this.transform.DOMoveY(posYToMove, .1f).SetEase(Ease.InSine).SetId("Knife").OnComplete(()=>WoodCenterController.instance.wood.OnFinishHit(this));
            this.isAbaliable = false;



        }
       
    }

    public void Init()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.transform.DOLocalMoveY(0, .1f).From(-2);
    }

    public void DeactivateCollision( bool boxCollision = false,bool poligonCollision = false) {

        colliderBox.enabled =  boxCollision;
        polygonCol.enabled =  poligonCollision; 
        
       
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Knife" && !isOnTheWood )
        {
            
            this.transform.DOKill(false);

            
            SoundManager.instance.PlayVFX("KnifeHit1");

            EventManagerController.instance.Lost(this);

            DeactivateCollision();
            rb.AddForce(Vector2.down*10);
            this.rb.gravityScale = 1;
            this.rb.AddTorque(Random.Range(-360, 360) * 0.1f, ForceMode2D.Impulse);
            if (GameManager.instance.vibrationOn)
            {
                Vibration.VibratePeek();
            }

        }
    }


    public void DestroyEvent(Object sender) {
       transform.SetParent(null);

       DeactivateCollision();
       this.rb.gravityScale = 1f;
       this.rb.AddTorque(Random.Range(-360,360)*0.01f, ForceMode2D.Impulse);
       this.rb.AddForce(new Vector2(Random.Range(0, 1), Random.Range(0, 1)) * 10f, ForceMode2D.Impulse);
       this.rb.AddForce((transform.position - WoodCenterController.instance.transform.position).normalized * 200);
       DestroyAfterDelay(3f);
       isOnTheWood = false;

    }

   
    public void DestroyAfterDelay(float delay) {
     
        Destroy(this.gameObject, delay);
    }
    public void DestroyImidiate(Object sender)
    {
            Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        EventManagerController.instance.WoodBrokeAction += DestroyEvent;
        EventManagerController.instance.ClearAllTrushAction += DestroyImidiate;
    }

    private void OnDisable()
    {
        EventManagerController.instance.WoodBrokeAction -= DestroyEvent;
        EventManagerController.instance.ClearAllTrushAction -= DestroyImidiate;
    }
}
