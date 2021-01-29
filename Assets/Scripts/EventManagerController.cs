using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManagerController : MonoBehaviour
{

    public static EventManagerController instance;
  

    public UnityAction RoundFinishedAction;
    public UnityAction RoundStartAction;
    public UnityAction ClearAllTrushAction;
    public UnityAction LostAction;
    public UnityAction BonusHitAction;
    public UnityAction HitWoodAction;
    public UnityAction WoodBrokeAction;
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        
    }
    public void RoundFinished() {
        RoundFinishedAction?.Invoke();
    }
    public void RoundStart()
    {
        Debug.Log("Round Started");
        RoundStartAction?.Invoke();
    }

    public void ClearTrush()
    {
        ClearAllTrushAction?.Invoke();
    }
    public void Lost()
    {
        LostAction?.Invoke();
    }

    public void BonusHit() { 
        BonusHitAction?.Invoke();
    }


    public void HitWood()
    {
        HitWoodAction?.Invoke();

    }  
    public void WoodBroke()
        {
            WoodBrokeAction?.Invoke();
        }
    }