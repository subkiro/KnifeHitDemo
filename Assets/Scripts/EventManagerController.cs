using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManagerController : MonoBehaviour
{

    public static EventManagerController instance;
  

    public UnityAction RoundFinishedAction;
    public UnityAction RoundStartAction;
    public UnityAction<Object> ClearAllTrushAction;
    public UnityAction LostAction;
    public UnityAction<Object> BonusHitAction;
    public UnityAction HitWoodAction;
    public UnityAction<Object> WoodBrokeAction;
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        
    }
    public void RoundFinished(Object sender) {
        Debug.Log(sender.name + " --- RoundFinished");
        RoundFinishedAction?.Invoke();
    }
    public void RoundStart(Object sender)
    {
        Debug.Log(sender.name + " --- RoundStart");
        RoundStartAction?.Invoke();
    }

    public void ClearTrush(Object sender)
    {
        Debug.Log(sender.name + " --- ClearTrush");
        ClearAllTrushAction?.Invoke(this);
    }
    public void Lost(Object sender)
    {
        Debug.Log(sender.name + " --- Round Lost");
        LostAction?.Invoke();
    }

    public void BonusHit(Object sender) {
        Debug.Log(sender.name + " --- BonusHit");
        BonusHitAction?.Invoke(this);
    }


    public void HitWood(Object sender)
    {
        
        

        Debug.Log(sender.name + " --- HitWood");
        HitWoodAction?.Invoke();

    }  
    public void WoodBroke(Object sender)
        {
        Debug.Log(sender.name+" --- WoodBroke");
        WoodBrokeAction?.Invoke(this);
        }
    }