using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WoodCenterObject : MonoBehaviour
{
   
    public float fieldOfImpact=-1.22f;
    public float forceOnExplode = 1;
    public int counter = 0;
    [SerializeField] private SpriteRenderer hitWood;



    private void Awake()
    {
        fieldOfImpact = 1.22f;
        
    }
    // Update is called once per frame
    public void Init( float RotSpeed = 5f, DG.Tweening.RotateMode RotateMode =RotateMode.FastBeyond360) {
       
        counter = GameManager.instance.MaxHit;
        this.transform.DORotate(new Vector3(0,0, 360), RotSpeed, RotateMode).SetLoops(-1).SetEase(Ease.Linear);
        
    }


    public void Explode() {

        SoundManager.instance.PlayVFX("ExplodeSound1");
        Destroy(this.gameObject);

    }

    

    public void TriggerCounter() {
      
        --counter;
        if (counter == 0)
        {
            EventManagerController.instance.RoundFinished();
        }


    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }

    public void OnFinishHit(Knife knife)
    {
            SoundManager.instance.PlayVFX("HitSound"+Random.Range(1,4));
            EventManagerController.instance.HitWood();
            KnifeController.instance.DecreaseKnifes();
            OnHitVFX();
           
            knife.DeactivateCollision(0, true,0,0.05f,1,true);
            knife.isOnTheWood = true;
            knife.transform.SetParent(this.transform);
            TriggerCounter();


    }
    public void OnHitVFX() {
        this.transform.DOPunchPosition(new Vector3(0, 0.1f, 0), 0.1f, 1);
        hitWood.gameObject.SetActive(true);
        hitWood.DOFade(0, 0.2f).From(.6f).SetEase(Ease.Flash).OnComplete(() => hitWood.gameObject.SetActive(false));
    }

    public void ShowVfx()
    {
        
        SoundManager.instance.PlayVFX("OpenWindow1");
        transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero);
        Init();
        
    }

    private void OnDestroy()
    {
        transform.DOKill();
        EventManagerController.instance.ClearTrush();


    }
}
