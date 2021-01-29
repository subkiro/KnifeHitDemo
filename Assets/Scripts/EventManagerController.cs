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
        Debug.Log("RoundFinished");
        RoundFinishedAction?.Invoke();
    }
    public void RoundStart()
    {
        Debug.Log("RoundStart");
        RoundStartAction?.Invoke();
    }

    public void ClearTrush()
    {
        Debug.Log("ClearTrush");
        ClearAllTrushAction?.Invoke();
    }
    public void Lost()
    {
        Debug.Log("Round Lost");
        LostAction?.Invoke();
    }

    public void BonusHit() {
        Debug.Log("BonusHit");
        BonusHitAction?.Invoke();
    }


    public void HitWood()
    {
        Debug.Log("HitWood");
        HitWoodAction?.Invoke();

    }  
    public void WoodBroke()
        {
        Debug.Log("WoodBroke");
        WoodBrokeAction?.Invoke();
        }
    }