﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WoodCenterObject : MonoBehaviour
{
   
    
    public float forceOnExplode = 1;
    public int counter = 0;
    [SerializeField] private SpriteRenderer hitWood;



    // Update is called once per frame
    public void Init( float RotSpeed = 5f, DG.Tweening.RotateMode RotateMode =RotateMode.FastBeyond360) {
       
        counter = GameManager.instance.MaxHit;
        this.transform.DORotate(new Vector3(0,0, 360), RotSpeed, RotateMode).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOScale(Vector3.one, 0.2f).From(Vector3.one*0.1f).OnComplete(()=>SoundManager.instance.PlayVFX("HitSound4"));
    }


    

    

    public void TriggerCounter() {
        --counter;

        if (counter <= 0)
        {
            EventManagerController.instance.WoodBroke(this);
        }


    }





    

    public void OnFinishHit(Knife knife)
    {
        
            SoundManager.instance.PlayVFX("HitSound"+Random.Range(1,4));
           
            OnHitVFX();
           
            knife.DeactivateCollision( true,false);
           
            knife.transform.SetParent(this.transform);
            knife.isOnTheWood = true;
            TriggerCounter();
            EventManagerController.instance.HitWood(this);


    }
    public void OnHitVFX() {
        this.transform.DOPunchPosition(new Vector3(0, 0.1f, 0), 0.1f, 1);
        hitWood.gameObject.SetActive(true);
        hitWood.DOFade(0, 0.2f).From(.6f).SetEase(Ease.Flash).OnComplete(() => hitWood.gameObject.SetActive(false));
    }

    public void ShowVfx()
    {
        
        SoundManager.instance.PlayVFX("OpenWindow1");
        
        Init();
        
    }

    public void OnDestroyBroke()
    {
        transform.DOKill();
        SoundManager.instance.PlayVFX("ExplodeSound1");
        EventManagerController.instance.RoundFinished(this);
        Destroy(gameObject);

    }
    public void DeactivateCollider()
    {

       
    }
    private void OnEnable()
    {
        EventManagerController.instance.LostAction += DeactivateCollider;
    }
    private void OnDisable()
    {
        EventManagerController.instance.LostAction -= DeactivateCollider;
    }
    
}
